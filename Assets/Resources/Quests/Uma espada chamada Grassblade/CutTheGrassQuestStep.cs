using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTheGrassQuestStep : QuestStep
{
    public GameObject grassPrefab;

    private void OnEnable(){
        tileDestrutivel.TileDestroyed += OnTileDestroyed;  // Define esse método como listener desse evento
    }
    private void OnDisable(){
        tileDestrutivel.TileDestroyed -= OnTileDestroyed;
    }

    private void Start(){
        int totalCount = HowManyGrassLeft(grassPrefab);
        Debug.Log("Quantidade de gramas no mapa: "+ totalCount);        
    }

    private int HowManyGrassLeft(GameObject prefab){
        GameObject[] objects = FindObjectsOfType<GameObject>(); // Lista de todos os objetos na Scene

        int count = 0;
        foreach(GameObject obj in objects){
            if(obj.activeInHierarchy && obj.name.Contains("Grama Cortável")){ // Verifica, entre os objetos ativos, aqueles que contêm "Grama Cortável" no nome.
                    count++;
            }
        }
        return count;
    }

    // Método para ser chamado quando um tile for destruído
    public void OnTileDestroyed(GameObject destroyedTile){
        int count = HowManyGrassLeft(grassPrefab);
        Debug.Log("Ainda existem "+ count + " gramas no mapa.");
        UpdateState(count);
        if(count >= 100){
            Debug.Log("Já cortou muita grama! FinishQuestStep();");
            FinishQuestStep();
        }
    }

    private void UpdateState(int count){
        string state = count.ToString();
        ChangeState(state);
    }

    /*protected override void SetQuestStepState(string state){ Parei de seguir o tutorial no minuto 56, quando ele começa
    a fazer o sistema de Load.
        this.count = System.Int32.Parse(state);
    }*/
}
