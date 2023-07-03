using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { // Único para cada Quest. O id é o próprio nome do arquivo SO
        get;
        private set;
    }

    [Header("Geral")]
    public string displayName; // Nome da Quest como será exibido.

    [Header("Requerimentos")]
    public QuestInfoSO[] questPrerequisites; // Lista de pré-requisitos da Quest.

    [Header("Etapas")]
    public GameObject[] questStepPrefabs; // Lista de prefabs que representam as etapas da Quest

    // Recompensas etc.

    private void OnValidate(){ // Método nativo do Editor. Altera o valor de "id" para o nome do SO.
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}

/* Para acessar as informações das Quests, utilizar os seguintes comandos:
QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");  

Acessa os scripts na pasta Resources/Quests

*/