using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plinio : AdversarioScript, ISwitch
{
    //private Animator animator;
    public bool estado;
    private int layerP;

    public void Toggle(){
        estado = !estado;
        animator.SetBool("ativado", estado);
        if(!audiosource.isPlaying){
            audiosource.PlayOneShot(enemySfx[0]);
        }

        layerP = gameObject.layer;

        if(layerP == 7){
            gameObject.layer = 0;
        }
        if(layerP == 0){
            gameObject.layer = 7;
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }
}
