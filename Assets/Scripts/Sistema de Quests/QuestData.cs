using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Informações relativas à Quest que deverão permanecer guardadas quando o jogo fechar.
*/
[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int questStepIndex;
    public QuestStepState[] questStepStates;

    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates){
        this.state = state;
        this.questStepIndex = questStepIndex;
        this.questStepStates = questStepStates;
    }
}

/*
    A Classe Quest possui o método público QuestData GetQuestData().
*/