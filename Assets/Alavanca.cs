using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour, ISwitch
{
    public bool estado;
    Animator animator;
    public GameObject other;

    public void Toggle(){
        // Alavanca:
        estado = !estado;
        animator.SetBool("ligado", estado);

        // Objeto afetado pela alavanca:
        ISwitch obj = other.GetComponent<ISwitch>();
        obj.Toggle();
    }
    private void Awake() {
        animator = GetComponent<Animator>();
    }
}
