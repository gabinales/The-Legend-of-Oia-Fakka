using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour, ISwitch
{
    public bool ligado;
    Animator animator;

    public void Toggle(){
        ligado = !ligado;
        animator.SetBool("ligado", ligado);
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }
}
