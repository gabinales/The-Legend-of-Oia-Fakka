using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// O controlador decide entre os estados CORRENDO e FALANDO, etc.
public enum GameState
{ // Todos os estados possíveis.
    MovimentacaoLivre,
    Dialogo,
    Loja
}

public class GameController : MonoBehaviour
{    
    
    [SerializeField] playerController pController;
    public GameObject JanelaPause;

    // Para trocar entre os diferentes estados:
    public GameState state;

    DamageHandler DamageHandler;

    // Patrick 24.06 --- Estado Pausado
    private bool isPaused = false;
    private bool isTalking = false;


    private void Start()
    {
        Time.timeScale = 1f;

        DamageHandler = GetComponent<DamageHandler>();
    }
    private void Awake(){
        /*
        if(instance != null){
            Debug.LogError("Mais de um Game Controller na cena");
        }
        instance = this;
        */
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            TogglePause();
        }

        if(isPaused){
            return; // Evita a execução do código abaixo se estiver pausado
        }

        if (state == GameState.MovimentacaoLivre)
        {
            pController.HandleUpdate();
        }        
    }

    private void TogglePause(){
        isPaused = !isPaused;
        Debug.Log("Jogo pausado");

        if(isPaused == true){
            JanelaPause.SetActive(true);
            Time.timeScale = 0.005f; // QUASE para o tempo
        }
        if(isPaused == false){
            JanelaPause.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
