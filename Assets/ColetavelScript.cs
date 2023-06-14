using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class ColetavelScript : MonoBehaviour
{
    public string nomeColetavel;

    public GameObject otherObject;
    Animator otherAnimator;

    void Awake(){
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.V)){
                Destroy(gameObject);
                // adicionarItemInventario();
                otherAnimator.ResetTrigger("Desarmado");
            }
        }
    }
    /*public void Pegar(InputAction.CallbackContext context){
        Debug.Log("PEGOU");
    }*/
}
    

