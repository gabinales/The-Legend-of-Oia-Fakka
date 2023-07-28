using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAtacavel
{
    public PlayerStats player;
    [Header("Geral")]
    public int hpMax;
    public int hpAtual;
    public int defesa;
    public int danoBase; // descomentei
    public int danoToque;
    public string nomeAdversario;
    public float velocidade;
    public Animator animator;

    public void Dano(int quantidade){
        Debug.Log("Dano");
    }
    public void Cura(int quantidade){
        Debug.Log("Cura");
    }

    private void OnCollisionEnter2D(Collision2D colisor){
        if(colisor.gameObject.CompareTag("Player")){
            IAtacavel IAtacavel = colisor.gameObject.GetComponent<IAtacavel>();
            if(IAtacavel != null){
                IAtacavel.Dano(danoToque);
                cameraController.instance.ScreenKick();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.danoHit, this.transform.position);
            }
        }
    }
}
