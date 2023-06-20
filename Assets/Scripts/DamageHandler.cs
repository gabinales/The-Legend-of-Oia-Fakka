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
    private TMP_Text valorDano;
    public GameObject hpDisplay;

    public void Damage(int dano, GameObject objeto)
    {
        if (objeto.CompareTag("enemy"))
        {
            if(dano >= objeto.GetComponent<Cohen>().hpAtual){
                objeto.GetComponent<Cohen>().SfxDefeated();
                objeto.GetComponent<Cohen>().hpAtual -= dano;
            }
            objeto.GetComponent<Cohen>().SfxDamaged();
            objeto.GetComponent<Cohen>().hpAtual -= dano;

            DamageIndicator indicator = Instantiate(hpDisplay, objeto.transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.textMeshPro.text = dano.ToString();

            // dano pop-up
/*             canvasRigidBody = objeto.transform.GetChild(0).GetComponentInChildren<Rigidbody2D>();
            valorDano = objeto.GetComponentInChildren<TMP_Text>();
            valorDano.text = dano.ToString();
            canvasRigidBody.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
            canvasRigidBody.gravityScale = 1f; */
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

