using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plinio : AdversarioScript, ISwitch
{
    private Animator animator;
    public bool estado;

    public void Toggle(){
        estado = !estado;
        animator.SetBool("ativado", estado);
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }
}
