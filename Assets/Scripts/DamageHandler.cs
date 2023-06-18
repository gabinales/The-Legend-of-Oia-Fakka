using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.Events;

//Este script serve para definir a funçao dano que acessa o hp do objeto de colisao e modifica o valor
//Também pode servir para curar (falta implementar Heal();)

public class DamageHandler : MonoBehaviour
{
    // Valor do dano saltando na tela.
    // futuramente mudar isso para sair do bixo e nao da arma
    private Rigidbody2D canvasRigidBody;
    private RectTransform canvasPosition;
    private float VelYInicial = 5f;
    private float VelXInicialIntervalo = 3f;
    private TMP_Text valorDano;

    public void Damage(int dano, GameObject objeto)
    {
        objeto.GetComponent<Cohen>().HP -= dano;

        if (objeto.CompareTag("enemy"))
        {
            // Damage pop-up
            canvasRigidBody = objeto.transform.GetChild(0).GetComponentInChildren<Rigidbody2D>();
            valorDano = objeto.GetComponentInChildren<TMP_Text>();
            valorDano.text = dano.ToString();
            canvasRigidBody.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
            canvasRigidBody.gravityScale = 1f;

            /*  canvasPosition = objeto.GetComponentInChildren<RectTransform>();
             canvasPosition.anchoredPosition3D = Vector3.up; */
        }

    }
    public void resetaDisplay(GameObject objeto)
    {
        canvasRigidBody = objeto.transform.GetChild(0).GetComponentInChildren<Rigidbody2D>();
        canvasRigidBody.velocity = Vector3.zero;

        valorDano = objeto.GetComponentInChildren<TMP_Text>();
        valorDano.text = null;
        canvasRigidBody.gravityScale = 0f;
        canvasPosition = objeto.GetComponentInChildren<RectTransform>();
        canvasPosition.anchoredPosition3D = Vector3.up;

    }

   // public void Kill() => Destroy(GameObject objeto);

}



//public void Heal(int amount) => vida += amount;

//public void HealFull() => vida = _maxHp;

//public void Adjust(int value) => vida = value;

