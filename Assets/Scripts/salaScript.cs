using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salaScript : MonoBehaviour
{
    void OnTriggerStay2D (Collider2D other){
        if(other.CompareTag("Player")){
            cameraController.instance.SetPosition(new Vector2(transform.position.x, transform.position.y));

            // Patrick 16.06 --- Mudar "tema" da Tela.
        }
    }
}
