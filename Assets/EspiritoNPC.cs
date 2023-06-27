using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;

public class EspiritoNPC : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset inkJSON;

    //private Story inkStory;

    private bool spawned = false;

    private Dictionary<string, Ink.Runtime.Object> variables;

    Story inkStory;

    public void Start()
    {
        inkStory = new Story(inkJSON.text);
    }

    public void Update()
    {
        int spawn = ((Ink.Runtime.IntValue)DialogManager.Instance.GetVariable("SPAWN")).value;

        if (spawn == 1 && !spawned)
        {
            spawned = true;
            EnemySpawner enemySpawner = GetComponentInChildren<EnemySpawner>();
            enemySpawner.SpawnEnemies();
        }
    }

    /* public void ChecaVariaveisLocais()
    {
        //SPAWN é um bool definido no arquivo Ink "espirito"
        //converte Ink.Runtime.Object (valor da variavel bool SPAWN) para string 
        string value = inkStory.variablesState.GetVariableWithName("SPAWN").ToString();

        //if(value == "1")
        Debug.Log("SPAWN: " + value);

        /*   variables = new Dictionary<string, Ink.Runtime.Object>();
          foreach (string name in inkStory.variablesState)
          {
              Ink.Runtime.Object value = inkStory.variablesState.GetVariableWithName(name);
              variables.Add(name, value);
              Debug.Log("nova variável LOCAL adicionada: " + name + " = " + value);

              inkStory.variablesState.variableChangedEvent += (name, value) => Debug.Log("mudou variavel");

          } 
    } */


    public void Interacao()
    {
        string pokemonName = ((Ink.Runtime.StringValue)DialogManager.Instance.GetVariable("pokemon_name")).value;

        if (pokemonName == "Squirtle" && !spawned)
        {
            EnemySpawner enemySpawner = GetComponentInChildren<EnemySpawner>();
            enemySpawner.SpawnEnemies();
            spawned = true;
        }

        DialogManager.Instance.StartDialogue(inkJSON);
    }
}
