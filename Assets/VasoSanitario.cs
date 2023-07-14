using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoSanitario : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset vasoSanitario;

    private AudioSource audiosource;
    public AudioClip clip;

    private void Start(){
        audiosource = GetComponent<AudioSource>();
    }
    public void Interacao(){
        DialogManager.Instance.StartDialogue(vasoSanitario);
        
        if(!audiosource.isPlaying){
            audiosource.PlayOneShot(clip);
        }
    }
}
