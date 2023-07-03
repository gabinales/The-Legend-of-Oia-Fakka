using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    // Informações estáticas da Quest:
    public QuestInfoSO info;

    // Estado de progressão da Quest:
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    // Construtor para passar a informação do SO para essa Quest:
    public Quest(QuestInfoSO questInfo){ // (Toda Quest é inicializada com essas condições)
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for(int i = 0; i < questStepStates.Length; i++){
            questStepStates[i] = new QuestStepState();
        }
    }

    // Método para avançar para a próxima etapa:
    public void MoveToNextStep(){
        currentQuestStepIndex++;
    }
    // Verifica se há, de fato, uma próxima etapa:
    public bool CurrentStepExists(){
        return(currentQuestStepIndex < info.questStepPrefabs.Length);
    }
    // Para instanciar facilmente qualquer que seja a etapa atual da Quest na Scene:
    public void InstantiateCurrentQuestStep(Transform parentTransform){
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if(questStepPrefab != null){
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex);
        }
    }
    private GameObject GetCurrentQuestStepPrefab(){
        GameObject questStepPrefab = null;
        if(CurrentStepExists()){
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else{
            Debug.LogWarning("Tentou pegar o quest step prefab, mas o stepIndex está fora do alcance "
            + "indicando que não há uma etapa atual: QuestId = " + info.id + ", stepIndex = " + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex){
        if(stepIndex < questStepStates.Length){
            questStepStates[stepIndex].state = questStepState.state;
        }
        else{
            Debug.LogWarning("Tentou acessar a informação de um quest step, mas o stepIndex está fora do limite: "
            + "Quest Id = " + info.id + ", Step Index = " + stepIndex);
        }
    }

    public QuestData GetQuestData(){
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
}


/* A Classe Quest é responsável por conter todas as informações relativas à Quest:
QuestInfoSO info
QuestState state
int currentQuestStepIndex
QuestStepState[] questStepStates

e outros métodos para interagir com a informação (instanciar as etapas, avançar etapas etc.).
*/