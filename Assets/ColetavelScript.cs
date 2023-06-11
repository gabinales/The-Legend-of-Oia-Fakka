using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ColetavelScript : MonoBehaviour
{
    public string nomeColetavel;

    public GameObject otherObject;
    Animator otherAnimator;

    void Awake(){
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.V)){
                Destroy(gameObject);
                // adicionarItemInventario();
                otherAnimator.ResetTrigger("Desarmado");
            }
        }
        else{
            Debug.Log("Deu errado");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
    

