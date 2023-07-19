using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData){
        AudioManager.instance.PlayOneShot(FMODEvents.instance.selecionaBotao, gameObject.transform.position);
    }
}
