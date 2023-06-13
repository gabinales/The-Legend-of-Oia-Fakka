using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyNPCController : MonoBehaviour
{
    private SceneChanger sceneChanger;

    private Transform escala;

    private void Awake(){
        escala = transform;
        
    }

    public void Interacao(){
        Debug.Log("INIMIGO");
    }
    
    void Update(){

    if(escala.localScale.y > 3){
        SceneManager.LoadScene("MINIGAME01");
    }
    }
}
