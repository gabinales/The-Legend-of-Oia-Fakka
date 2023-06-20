using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour, ISwitch
{
    public bool estado;
    Animator animator;
    public AudioSource audiosourceAlavanca;
    public AudioClip audioclipAlavanca;
    //public GameObject other;
    public List<GameObject> objectsList;

    public void Toggle(){
        // Alavanca:
        estado = !estado;
        animator.SetBool("ligado", estado);
        audiosourceAlavanca.PlayOneShot(audioclipAlavanca);

        // Objeto(s) afetado(s) pela alavanca:
        foreach(GameObject obj in objectsList){
            ISwitch switchObject = obj.GetComponent<ISwitch>();
            if(switchObject != null){
                switchObject.Toggle();
            }
        }

        /*ISwitch obj = other.GetComponent<ISwitch>();
        obj.Toggle();*/
    }
    private void Awake() {
        animator = GetComponent<Animator>();
        audiosourceAlavanca = GetComponent<AudioSource>();
    }
}
