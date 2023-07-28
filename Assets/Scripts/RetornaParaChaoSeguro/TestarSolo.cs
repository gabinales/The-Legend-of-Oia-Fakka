using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Testa se o jogador está sobre a layer Chao */

public class TestarSolo : MonoBehaviour
{
    [SerializeField] private float radius = 0.1f; // Raio da esfera para detecção de solo
    [SerializeField] private LayerMask whatIsGround;

    private void Start()
    {
        // Nenhuma alteração no Start, pode manter como estava
    }

    public bool IsGrounded()
    {
        // Executar um cast de esfera para baixo
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);

        // Verificar se houve colisão e a layer do objeto atingido
        if (hit != null && hit.gameObject != gameObject)
        {
            Debug.Log("tá seguro");
            return true;
        }
        else
        {
            Debug.Log("não tá seguro");
            return false;
        }
    }
}