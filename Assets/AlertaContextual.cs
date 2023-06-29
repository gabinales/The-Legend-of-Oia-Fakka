using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Tipo{
    Conversa, // Interações sociais
    Interacao,
    Surpresa,
    Pegar // Interações com itens
}

public class AlertaContextual : MonoBehaviour
{
    private GameObject otherObject; // Jogador
    private GameObject alertaContextual;

    public Tipo tipoDeAcao = new Tipo();

    private Animator alertaContextualAnimator;


    void Awake(){
        otherObject = GameObject.FindGameObjectWithTag("Player");
        if(otherObject != null){
            alertaContextual = otherObject.transform.Find("AlertaContextual").gameObject;

            if(alertaContextual != null){
                alertaContextualAnimator = alertaContextual.GetComponent<Animator>();
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            alertaContextual.SetActive(true);

            if(tipoDeAcao == Tipo.Conversa){
                alertaContextualAnimator.SetBool("conversa", true);
                alertaContextualAnimator.SetBool("interacao", false);
                alertaContextualAnimator.SetBool("surpresa", false);
                alertaContextualAnimator.SetBool("pegar", false);
            }
            if(tipoDeAcao == Tipo.Interacao){
                alertaContextualAnimator.SetBool("conversa", false);
                alertaContextualAnimator.SetBool("interacao", true);
                alertaContextualAnimator.SetBool("surpresa", false);
                alertaContextualAnimator.SetBool("pegar", false);
            }
            if(tipoDeAcao == Tipo.Surpresa){
                alertaContextualAnimator.SetBool("conversa", false);
                alertaContextualAnimator.SetBool("interacao", false);
                alertaContextualAnimator.SetBool("surpresa", true);
                alertaContextualAnimator.SetBool("pegar", false);
            }
            if(tipoDeAcao == Tipo.Pegar){
                alertaContextualAnimator.SetBool("conversa", false);
                alertaContextualAnimator.SetBool("interacao", false);
                alertaContextualAnimator.SetBool("surpresa", false);
                alertaContextualAnimator.SetBool("pegar", true);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            alertaContextual.SetActive(false);

            alertaContextualAnimator.SetBool("conversa", false);
            alertaContextualAnimator.SetBool("interacao", false);
            alertaContextualAnimator.SetBool("surpresa", false);
            alertaContextualAnimator.SetBool("pegar", true);
        }
    }
}