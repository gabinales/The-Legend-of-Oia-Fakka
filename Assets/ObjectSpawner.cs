using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objList;
    public List<Transform> objPositions;

    /*[Header("SFX")]
    public EventReference sfx;*/

    public void SpawnObjects(){
        int numPositions = Mathf.Min(objList.Count, objPositions.Count); // Retorna o menor de dois valores

        for(int i = 0; i < numPositions; i++){
            GameObject obj = Instantiate(objList[i], objPositions[i].position, Quaternion.identity);
            obj.transform.SetParent(transform); // Define o ObjectSpawner como pai dos objetos.
        }

        AudioManager.instance.PlayOneShot(FMODEvents.instance.ativaSpawner, this.transform.position);
    }
}
