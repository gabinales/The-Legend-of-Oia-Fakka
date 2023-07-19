using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasoSanitario : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset vasoSanitario;

    public void Interacao(){
        DialogManager.Instance.StartDialogue(vasoSanitario);
        
        AudioManager.instance.PlayOneShot(FMODEvents.instance.toiletFlush, this.transform.position);
    }
}
