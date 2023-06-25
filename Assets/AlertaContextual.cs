using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlertaContextual : MonoBehaviour
{
    public GameObject alerta;

    private bool playerInRange;

    void Awake()
    {
        alerta.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !DialogManager.Instance.dialogoOcorrendo)
        {
            alerta.SetActive(true);
        }
        else alerta.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}


