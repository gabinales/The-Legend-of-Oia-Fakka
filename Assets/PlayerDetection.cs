using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetection : MonoBehaviour
{
    private bool playerInRange;
    private bool interagido;

    void Awake()
    {
        interagido = false;
    }

    //DETECTA PROXIMIDADE
    //SUBSTITUIR POR OVERLAPCIRCLE, JÁ QUE ONTRIGGERENTER EXIGE QUE TENHA UM BOX COLLIDER
    //MUITO GRANDE
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!DialogManager.Instance.dialogoOcorrendo && !interagido)
            {
                gameObject.GetComponent<iInteragivel>().Interacao();
                interagido = true;
            }
        }
    }
}

