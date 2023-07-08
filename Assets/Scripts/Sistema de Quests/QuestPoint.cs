using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* O QuestPoint representa os pontos no espaço que iniciam e/ou terminam uma Quest. 
Pode ser um NPC, um lugar etc.

Interagível: aperte C para Interacao()
*/

//[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour, iInteragivel
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")] //REFATORAR DEPOIS -----------------------------------------------<<<<
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    
    [Header("Chat")]
    public TextAsset inkJSON;
    public bool withChat;
    
    private bool playerIsNear = false;

    private string questId;
    private QuestState currentQuestState;

    private void Awake(){
        questId = questInfoForPoint.id;
    }

    // Inscreve/desinscreve o método QuestStateChange no evento em GameEventsManager:
    private void OnEnable(){
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable(){
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        // GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }
    public virtual void Interacao(){ // Virtual pode ser sobrescrita por override 
        if(!playerIsNear){           // numa Classe filha que herda QuestPoint.
            Debug.Log("[Quest Point]: O Quest Point associado a [" + questId + "] não está suficientemente perto para interagir.");
            return;         
        }
        
        // Inicia diálogo, se houver:
        if(withChat){
            DialogManager.Instance.StartDialogue(inkJSON);
            return;
        }
        else if(!withChat){
            Debug.Log("Não tem diálogo para esse NPC/Quest Point.");
        }
        // Inicia o/ou termina a Quest de acordo com as Configurações
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint){
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        else if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint){
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest){
        // Antes, verifica se este quest point representa a quest correspondente:
        if(quest.info.id.Equals(questId)){
            currentQuestState = quest.state;
            Debug.Log("Quest com o id " + questId + " foi atualizada para o estado: " + currentQuestState);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerIsNear = false;
        }
    }
}
