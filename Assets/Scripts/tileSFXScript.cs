using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileSFXScript : MonoBehaviour
{
    public AudioClip tileSFX;
    private bool jogadorEmCima = false;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            jogadorEmCima = true;
            TocaSFXPassos();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            jogadorEmCima = false;
        }
    }
    private void TocaSFXPassos(){
        if(jogadorEmCima && tileSFX != null){
            AudioSource.PlayClipAtPoint(tileSFX, transform.position);
        }
    }
}
