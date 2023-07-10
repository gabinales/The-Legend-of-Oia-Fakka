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

    public void Interacao()
    {
  /*       int spawn = ((Ink.Runtime.IntValue)DialogManager.Instance.GetVariable("SPAWN")).value;

        if (spawn == 1 && !spawned)
        {
            EnemySpawner enemySpawner = GetComponentInChildren<EnemySpawner>();
            enemySpawner.SpawnEnemies();
            spawned = true;
        }
 */
        DialogManager.Instance.StartDialogue(inkJSON);
    }
}
