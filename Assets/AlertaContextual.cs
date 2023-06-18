using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Tipo{
    Conversa,
    Interacao
}

public class AlertaContextual : MonoBehaviour
{
    public GameObject otherObject;
    public GameObject alertaContextual;
    
    public Tipo tipoDeAcao = new Tipo();

    public Animator alertaContextualAnimator;


    void Awake(){
        alertaContextualAnimator = alertaContextual.GetComponent<Animator>();
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            alertaContextual.SetActive(true);
            
            if(tipoDeAcao == Tipo.Conversa){
                alertaContextualAnimator.SetBool("conversa", true);
                alertaContextualAnimator.SetBool("interacao", false);
            }
            if(tipoDeAcao == Tipo.Interacao){
                alertaContextualAnimator.SetBool("conversa", false);
                alertaContextualAnimator.SetBool("interacao", true);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            alertaContextual.SetActive(false);
        }
    }
}
    

