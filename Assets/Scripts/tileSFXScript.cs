using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileSFXScript : MonoBehaviour
{
    public AudioClip[] sfxArray;

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player")){
            TocaSFX(0);
        }
        if(collision.gameObject.CompareTag("weapon")){
            int randomNumber = Random.Range(1,3);
            TocaSFX(randomNumber);
        }
    }

    private void TocaSFX(int index){
        AudioSource.PlayClipAtPoint(sfxArray[index], transform.position);
        //Debug.Log(sfxArray[index]);
    }
}
