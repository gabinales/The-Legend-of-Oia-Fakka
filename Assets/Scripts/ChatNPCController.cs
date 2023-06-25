using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatNPCController : MonoBehaviour, iInteragivel // Explicita que esta classe é um NPC interagível
{
    [SerializeField] private TextAsset inkJSON;

    public void Interacao()
    {
        DialogManager.Instance.StartDialogue(inkJSON); // Chama uma instância do Dialog Manager
    }
}
