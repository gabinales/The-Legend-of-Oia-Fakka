using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileSFXScript : MonoBehaviour
{
    private AudioSource TileSFX;
    public AudioClip clip;

    private void Start(){
        TileSFX = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            TocaTileSFX();
        }
    }

    private void TocaTileSFX(){
        TileSFX.pitch = (Random.Range(0.7f, 2.5f));
        TileSFX.PlayOneShot(clip);
    }
}
