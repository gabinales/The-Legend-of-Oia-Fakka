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

    [SerializeField] private GameObject globalLight;
    private UnityEngine.Rendering.Universal.Light2D light2D;

    public int timeOfDay = 0; // 0 = dia, 1 = tarde, 2 = noite

    // Para trocar entre os diferentes estados:
    public GameState state;

    DamageHandler DamageHandler;

    // Patrick 24.06 --- Estado Pausado
    private bool isPaused = false;
    private bool isTalking = false;


    private void Start()
    {
        light2D = globalLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        Time.timeScale = 1f;

        DamageHandler = GetComponent<DamageHandler>();
    }
    private void Awake()
    {
        /*
        if(instance != null){
            Debug.LogError("Mais de um Game Controller na cena");
        }
        instance = this;
        */
    }

    private void Update()
    {
        switch (timeOfDay)
        {
            case 0:
                light2D.intensity = 0.5f;
                //light2D.color = Color.yellow;
                break;
            case 1:
                //light2D.color = new Color(166, 217, 221);
                light2D.intensity = 0.8f;
                break;
            case 2:
                light2D.intensity = 0.3f;
                //light2D.color = Color.blue;
                break;
            default:
                Debug.Log("time of day fora do range");
                break;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TogglePause();
        }

        if (isPaused)
        {
            return; // Evita a execução do código abaixo se estiver pausado
        }

        if (state == GameState.MovimentacaoLivre)
        {
            pController.HandleUpdate();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Debug.Log("Jogo pausado");

        if (isPaused == true)
        {
            JanelaPause.SetActive(true);
            Time.timeScale = 0.005f; // QUASE para o tempo
        }
        if (isPaused == false)
        {
            JanelaPause.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
