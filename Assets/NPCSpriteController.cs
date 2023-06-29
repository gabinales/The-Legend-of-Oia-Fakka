using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteController : MonoBehaviour
{
    public Transform player;
    private Animator animator;
    private bool withinRange = false;
    public float areaDeVisao;

    private void Awake(){
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }
    
    private void Update(){
        if(withinRange){  // Condição para o NPC "fixar" a visão no jogador: estar dentro do alcance. 
            MatchDirection();
        }
        CheckRange();
    }
    
    private void CheckRange(){
        if(Vector3.Distance(player.position, transform.position) <= areaDeVisao){
            withinRange = true;
        }
        else{
            withinRange = false;
        }
    }
    
    private void MatchDirection(){ // Para o NPC olhar na direção do jogador
        Vector3 direcao = player.transform.position - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        if(angulo > -45f && angulo <= 45f){
            // Direita
            animator.SetFloat("moveX", 1f);
            animator.SetFloat("moveY", 0f);
        }
        else if(angulo > 45f && angulo <= 135f){
            // Cima
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", 1f);
        }
        else if(angulo > 135f || angulo <= -135f){
            // Esquerda
            animator.SetFloat("moveX", -1f);
            animator.SetFloat("moveY", 0f);
        }
        else if(angulo > -135f && angulo <= -45f){
            // Baixo
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", -1f);
        }
    }
}
