using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class ColetavelScript : MonoBehaviour
{
    public string nomeColetavel;

    public GameObject otherObject;
    public GameObject alertaContextual;
    TextMeshPro textoContextual;
    Animator otherAnimator;

    void Awake(){
        textoContextual= alertaContextual.GetComponentInChildren<TextMeshPro>();
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            alertaContextual.SetActive(true);
            textoContextual.text = "Aperte V.";

            if(Input.GetKeyDown(KeyCode.V)){
                alertaContextual.SetActive(false);
                textoContextual.text = "";
                
                Destroy(gameObject);
                otherAnimator.ResetTrigger("Desarmado");
            }
        }
        else{
            alertaContextual.SetActive(false);
        }
    }
}
    

