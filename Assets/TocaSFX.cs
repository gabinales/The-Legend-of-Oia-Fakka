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
    public bool ExecutarNoStart;

    private void Start(){
        if(ExecutarNoStart){
            Toca();
        }
    }

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
