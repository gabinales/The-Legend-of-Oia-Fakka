using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaScene : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 novaPosicaoInicial;
    public VetorPosicaoInicialPlayer posicaoPlayer;

    public void CarregarNovaCena(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            posicaoPlayer.posicaoInicial = novaPosicaoInicial; 
            CarregarNovaCena();
        }
    }
}
