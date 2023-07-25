using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePisoSecreto : MonoBehaviour
{
    private ObjectSpawner spawner;
    private GameObject gramaSecreta;
    private bool foiAtivado = false;

    void Start(){
        spawner = GetComponent<ObjectSpawner>();
        gramaSecreta = GameObject.Find("Grama Secreta");
    }
    

    void Update(){
        if(gramaSecreta == null && !foiAtivado){
            AudioManager.instance.PlayOneShot(FMODEvents.instance.puzzleResolvido, this.transform.position);
            AtivaSpawner();
        }
    }

    public void AtivaSpawner(){
        if(spawner != null){
            spawner.SpawnObjects();

            foiAtivado = true;
        }
    }
}
