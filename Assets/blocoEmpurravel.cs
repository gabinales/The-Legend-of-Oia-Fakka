using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocoEmpurravel : MonoBehaviour
{
    private bool sendoEmpurrado = false;
    private Vector3 destino;
    private float velocidade = 1f;
    private float velocidadeMultiplicador = 1.5f;


    public void Empurra(Vector3 direcao, float vel){
        if(!sendoEmpurrado){
            if(ChecaDirecao(direcao)){
                destino = transform.position + direcao;
                vel = vel * velocidadeMultiplicador;
                sendoEmpurrado = true;
            }
        }
    }

    private void Update() {
        if (Vector3.Distance(transform.position, destino) < Mathf.Epsilon){
            transform.position = destino;
            sendoEmpurrado = false;
        }
        else if(sendoEmpurrado){
            Debug.Log("sendo empurrado");
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidade * Time.deltaTime);
        }
    }

    private bool ChecaDirecao(Vector3 direcao){
        if(Physics2D.Raycast(
            transform.position,
            direcao)){
                return false;
            }
        return true;
    }
}
