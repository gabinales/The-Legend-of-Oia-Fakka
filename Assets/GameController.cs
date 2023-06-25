using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        /* if(DialogManager.Instance.dialogoOcorrendo)
        {
            state = GameState.Dialogo; // Muda o estado do jogo para DIALOGO
        };
        DialogManager.Instance.OnOcultaDialogo += () =>
        {
            if (state == GameState.Dialogo)
            {
                state = GameState.MovimentacaoLivre; // Retorna o jogo para o modo de movimentação livre
            }
        }; */
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            TogglePause();
        }

        if(isPaused){
            return;
        }

        if (state == GameState.MovimentacaoLivre)
        {
            pController.HandleUpdate();
        }        
        
        else if (state == GameState.Loja)
        {
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
        
        // Alternativa: transicionar entre os diferentes gamestates
        /*if(isPaused){
            if(state == GameState.MovimentacaoLivre){
                pController.enabled = false;
            }
            if(state == GameState.Dialogo){
                DialogManager.Instance.enabled = false;
            }
            // if(state == GameState.Loja ....)
        }
        else{
            if(state == GameState.MovimentacaoLivre){
                pController.enabled = true;
            }
            if(state == GameState.Dialogo){
                DialogManager.Instance.enabled = true;
            }
        }*/
    }
}
