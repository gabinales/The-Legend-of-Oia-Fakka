using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerPlaca : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset inkJSON;

    public void Interacao(){
        DialogManager.Instance.StartDialogue(inkJSON);
    }
}
