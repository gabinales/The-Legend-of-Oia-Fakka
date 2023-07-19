using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class TocaSFX : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private EventReference sfx;

    public bool ExecutarNoEnable;
    public bool ExecutarNoDisable;

    public void Toca(){
        AudioManager.instance.PlayOneShot(sfx, gameObject.transform.position);
    }

    private void OnEnable(){
        if(ExecutarNoEnable){
            Toca();
        }
    }
    private void OnDisable(){
        if(ExecutarNoDisable){
            Toca();
        }
    }
}
