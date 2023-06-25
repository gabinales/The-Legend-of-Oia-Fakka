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
    public Animator[] itemAnimators; // Array de Animators para animar os itens
    
    public GameObject[] pauseMenuEquipmentItems; // Array de objetos da janela de equipamentos (3)
    public GameObject[] pauseMenuInventoryItems; // Array de objetos da janela de itens (5)
    public GameObject[] pauseMenuOptionsItems; // Array de objetos da janela de opções (2)
    
    public GameObject[][] pauseMenuArrays; // Array multidimensional que armazena os três arrays

    private int currentArrayIndex = 0;
    private int selectedItemIndex = 0;
    
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

        pauseMenuArrays = new GameObject[][] {
            pauseMenuEquipmentItems,
            pauseMenuInventoryItems,
            pauseMenuOptionsItems
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            TogglePause();
        }

        if(isPaused){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                currentArrayIndex = (currentArrayIndex - 1 + pauseMenuArrays.Length) % pauseMenuArrays.Length;
                selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, pauseMenuArrays[currentArrayIndex].Length - 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)){
                currentArrayIndex = (currentArrayIndex + 1) % pauseMenuArrays.Length;
                selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, pauseMenuArrays[currentArrayIndex].Length - 1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                selectedItemIndex = (selectedItemIndex - 1 + pauseMenuArrays[currentArrayIndex].Length) % pauseMenuArrays[currentArrayIndex].Length;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)){
                selectedItemIndex = (selectedItemIndex + 1) % pauseMenuArrays[currentArrayIndex].Length;
            }

            // Destacar o item selecionado
            for (int i = 0; i < pauseMenuArrays.Length; i++){
                GameObject[] currentArray = pauseMenuArrays[i];
                for (int j = 0; j < currentArray.Length; j++){
                    if (i == currentArrayIndex && j == selectedItemIndex){
                        currentArray[j].GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 120f);
                    }
                    else{
                        currentArray[j].GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                    }
                }
            }
            return; // Evita a execução do código abaixo se estiver pausado
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
    }
}
