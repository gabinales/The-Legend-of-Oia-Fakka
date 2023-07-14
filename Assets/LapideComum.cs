using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapideComum : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset lapideComum;

    public void Interacao(){
        DialogManager.Instance.StartDialogue(lapideComum);
    }
}
