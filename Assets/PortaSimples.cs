using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaSimples : MonoBehaviour, iInteragivel
{
    public Animator animator;
    public float tempoFechamento = 3f;

    private bool estaAberta = false;
    private int layerPadrao; // Será utilizada temporariamente, para permitir a passagem do personagem.
    private int layerCorposSolidos;

    private void Start(){
        layerPadrao = LayerMask.NameToLayer("Default");
        layerCorposSolidos = LayerMask.NameToLayer("CorposSolidos");
    }

    public void Interacao(){
        if(!estaAberta){
            animator.SetTrigger("abriu");
            estaAberta = true;

            // Desbloqueia a passagem pelo tempo determinado em tempoFechamento:
            gameObject.layer = layerPadrao;
            Invoke("FecharPorta", tempoFechamento);
        }
    }

    private void FecharPorta(){
        animator.SetTrigger("fechou");
        estaAberta = false;

        // Bloqueia novamente a passagem:
        gameObject.layer = layerCorposSolidos;
    }
}
