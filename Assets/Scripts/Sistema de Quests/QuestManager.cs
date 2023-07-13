using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    O Quest Manager mapeia todas as Quests,
    que são acessadas pelo id

    O Quest Manager inicia, avança e finaliza as Quests.
    Quest events:
        onStartQuest <id>
        onAdvanceQuest <id>
        onFinishQuest <id>
    O evento QuestStateChange notifica as mudanças de estado na <quest>
    para todas as classes interessadas
*/

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    // Requerimentos para QuestStart aqui

    private void Awake(){
        questMap = CreateQuestMap();
    }
    // Inscreve os métodos nos eventos:
    private void OnEnable(){
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }
    private void OnDisable(){
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start(){
        // Compartilha o estato inicial de todas as quests
        foreach(Quest quest in questMap.Values){
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    // Método chamado para atualizar o estado da Quest e informar a todas as classes interessadas: 
    private void ChangeQuestState(string id, QuestState state){
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest){
        bool meetsRequirements = true;

        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites){
            if(GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED){
                meetsRequirements = false;
            }
        }
        return meetsRequirements;
    }

    private void Update(){
        // Loop por todas as Quests:
        foreach(Quest quest in questMap.Values){
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest)){
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    // Métodos para iniciar, avançar e finalizar uma quest:
    private void StartQuest(string id){
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        Debug.Log("Start Quest: " + id);
    }
    private void AdvanceQuest(string id){
        Quest quest = GetQuestById(id);
        // Avança a quest para a próxima etapa:
        quest.MoveToNextStep();

        // Se há mais etapas, instanciar a próxima:
        if(quest.CurrentStepExists()){
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // Se não há mais etapas, a quest pode terminar
        else{
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            //
        }
        Debug.Log("Advance Quest: " + id);
    }
    private void FinishQuest(string id){
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        Debug.Log("Finish Quest: " + id);
    }

    private void ClaimRewards(Quest quest){
        Debug.Log("GANHOU RECOMPENSA!!! (ou não)");
        // Adiciona as recompensas (se houver) no inventário:
        if(quest.info.recompensas != null){
            Inventory inventory = FindObjectOfType<Inventory>();
            foreach(ItemData recompensa in quest.info.recompensas){
                inventory.Add(recompensa);
            }
        }
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState){
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap(){
        // Carrega todos os QuestInfoSO do diretório Resources/Quests
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        // Cria o quest map: (Tem algumas adições para facilitar o debug)
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests){
            if(idToQuestMap.ContainsKey(questInfo.id)){
                Debug.LogWarning("Id duplicado encontrado ao criar quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id){ // Para encontrar eventuais erros com o id.
        Quest quest = questMap[id];
        if(quest == null){
            Debug.LogError("Id não encontrar no Quest Map: " + id);
        }
        return quest;
    }

    private void OnApplicationQuit(){
        foreach(Quest quest in questMap.Values){
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest){
        try{
            QuestData questData = quest.GetQuestData();
            // Serializa usando JsonUtility, mas poderia ser qualquer outro (como JSON.NET).
            string serializedData = JsonUtility.ToJson(questData);
            // Salva no PlayerPrefs, mas seria bom futuramente
            // um sistema de save e load mais robusto pra criar o arquivo de save.
            PlayerPrefs.SetString(quest.info.id, serializedData);
            
            Debug.Log(serializedData);
        }
        catch (System.Exception e){
            Debug.LogError("Falhou ao salvar a quest com o id " + quest.info.id + " : " + e);
        }
    }
}
