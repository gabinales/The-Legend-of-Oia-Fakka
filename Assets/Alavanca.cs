using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour, ISwitch
{
    public bool estado;
    Animator animator;
    public AudioSource audiosourceAlavanca;
    public AudioClip audioclipAlavanca;
    public List<GameObject> listaDeObjetos; // Lista de GameObjects afetados pela Alavanca

    private bool enemySpawnerAtivado = false;

    public void Toggle(){
        // Alavanca:
        estado = !estado;
        animator.SetBool("ligado", estado);
        audiosourceAlavanca.PlayOneShot(audioclipAlavanca);

        // Objeto(s) afetado(s) pela alavanca:
        foreach(GameObject obj in listaDeObjetos){
            ISwitch switchObject = obj.GetComponent<ISwitch>();
            if(switchObject != null){
                switchObject.Toggle();
            }
        }

        // Ativa o EnemySpawner apenas na primeira ativação da alavanca
        if(estado && !enemySpawnerAtivado){
            Debug.Log("Deveria spawnaer");
            EnemySpawner enemySpawner = GetComponentInChildren<EnemySpawner>();
            if(enemySpawner != null){
                Debug.Log("Cadee");
                enemySpawner.SpawnEnemies();
                enemySpawnerAtivado = true;
            }
        }
    }
    private void Awake() {
        animator = GetComponent<Animator>();
        audiosourceAlavanca = GetComponent<AudioSource>();
    }
}
