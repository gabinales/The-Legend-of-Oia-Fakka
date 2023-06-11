using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatNPCController : MonoBehaviour, NPC // Explicita que esta classe é um NPC interagível
{
    [SerializeField] Dialog dialogo;

    public void Interacao(){
        StartCoroutine(DialogManager.Instance.MostraDialogo(dialogo)); // Chama uma instância do Dialog Manager
    }
}
