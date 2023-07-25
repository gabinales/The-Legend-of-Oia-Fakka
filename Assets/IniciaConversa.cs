using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciaConversa : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset conversa;

    public void Interacao(){
        DialogManager.Instance.StartDialogue(conversa);
    }
}
