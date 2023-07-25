using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plinio : Enemy, ISwitch
{
    [Header("Plínio")]
    public bool estaAtivado;
    private int layerPlinio;
    private BoxCollider2D boxCollider;

    private void Awake(){
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Toggle(){
        estaAtivado = !estaAtivado;
        animator.SetBool("ativado", estaAtivado);
       
        // Toca SFX:
        AudioManager.instance.PlayOneShot(FMODEvents.instance.plinioToggle, this.transform.position);

        layerPlinio = gameObject.layer;

        if(layerPlinio == 9){
            gameObject.layer = 0;
            boxCollider.enabled = false;
        }
        if(layerPlinio == 0){
            gameObject.layer = 9;
            boxCollider.enabled = true;
        }
    }

    public void ReativaPlinio(){
        estaAtivado = true;
        animator.SetBool("ativado", estaAtivado);
        gameObject.layer = 9;
        boxCollider.enabled = true;
    }
}
