using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesList;
    public List<Transform> enemiesPositions;

    [Header("SFX")]
    public AudioSource audiosource;
    public AudioClip audioclip;

    public void SpawnEnemies(){
        int numPositions = Mathf.Min(enemiesList.Count, enemiesPositions.Count); // Retorna o menor de dois valores

        for(int i = 0; i < numPositions; i++){
            GameObject enemy = Instantiate(enemiesList[i], enemiesPositions[i].position, Quaternion.identity);
            enemy.transform.SetParent(transform); // Define o EnemySpawner como pai dos inimigos
        }

        if(!audiosource.isPlaying){
            audiosource.PlayOneShot(audioclip);
        }
    }
}
