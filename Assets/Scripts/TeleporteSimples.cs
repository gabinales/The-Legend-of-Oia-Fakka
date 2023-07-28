using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporteSimples : MonoBehaviour
{
    [Header("Geral")]
    private Transform posicaoDestino;
    public TeleporteSimples teleporteDestino;
    
    [Header("Configurações")]
    public bool teleporteAtivado;

    private void Start(){
        posicaoDestino = teleporteDestino.transform;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(teleporteAtivado && teleporteDestino != null){
            GameObject player = other.gameObject;
            if(player.CompareTag("Player")){
                teleporteDestino.teleporteAtivado = false;
                Teleportar(player.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(!teleporteAtivado){
            GameObject player = other.gameObject;
            if(player.CompareTag("Player")){
                teleporteAtivado = true;
            }
        }
    }

    void Teleportar(Transform objetoTeleportado){
        objetoTeleportado.position = posicaoDestino.position;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.usouTeleporte, this.transform.position);
        teleporteDestino.GetComponent<ParticleSystem>().Play();
    }
}
