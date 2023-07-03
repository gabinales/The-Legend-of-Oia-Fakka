using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract: explicita que esta Classe será herdada por outras Classes, e não utilizada diretamente.
public abstract class QuestStep : MonoBehaviour  // Passos comuns a todas as Quests:
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex){ // Chamado pela classe Quest
        this.questId = questId;
        this.stepIndex = stepIndex;
        //if(questStepState != null && questStepState != ""){
            //SetQuestStepState(questStepState);
            
        //}
        Debug.Log("SetQuestStepState está comentado.");
    }

    protected void FinishQuestStep(){
        if(!isFinished){
            isFinished = true;

            GameEventsManager.instance.questEvents.AdvanceQuest(questId);

            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState){
        GameEventsManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState));
    }

    //protected abstract void SetQuestStepState(string state);
}
