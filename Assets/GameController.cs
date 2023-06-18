using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

// O controlador decide entre os estados CORRENDO e FALANDO, etc.
public enum GameState
{ // Todos os estados possíveis.
    MovimentacaoLivre,
    Dialogo,
    Loja
}

public class GameController : MonoBehaviour
{
    [SerializeField] playerController controladorDoJogador;

    // Para trocar entre os diferentes estados:
    GameState state;

    DamageHandler DamageHandler;

    private void Start()
    {

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
        if (state == GameState.MovimentacaoLivre)
        {
            controladorDoJogador.HandleUpdate();
        }
        else if (state == GameState.Dialogo)
        {
            DialogManager.Instance.HandleUpdate(); // Criar função para que o DialogManager possa ser utilizado
        }
        else if (state == GameState.Loja)
        {
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DamageHandler.resetaDisplay(GameObject.Find("Cohen"));
        }
    }
}
