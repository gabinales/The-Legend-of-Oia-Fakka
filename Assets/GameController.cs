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

    // Para trocar entre os diferentes estados:
    GameState state;

    DamageHandler DamageHandler;

    // Patrick 24.06 --- Estado Pausado
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;

        DamageHandler = GetComponent<DamageHandler>();

        DialogManager.Instance.OnMostraDialogo += () =>
        {
            state = GameState.Dialogo; // Muda o estado do jogo para DIALOGO
        };
        DialogManager.Instance.OnOcultaDialogo += () =>
        {
            if (state == GameState.Dialogo)
            {
                state = GameState.MovimentacaoLivre; // Retorna o jogo para o modo de movimentação livre
            }
        };
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
        else if (state == GameState.Dialogo)
        {
            DialogManager.Instance.HandleUpdate(); // Cria função para que o DialogManager possa ser utilizado
        }
        else if (state == GameState.Loja)
        {
        }

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            DamageHandler.resetaDisplay(GameObject.Find("Cohen"));
        }*/
    }

    private void TogglePause(){
        isPaused = !isPaused;
        Debug.Log("Jogo pausado");

        if(isPaused == true){
            Time.timeScale = 0.005f;
        }
        if(isPaused == false){
            Time.timeScale = 1f;
        }
        

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
