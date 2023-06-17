using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageHandler : MonoBehaviour
{
    private Rigidbody2D canvas; // O Canvas pai do Texto contém um Rigidbody2D
    private TMP_Text valorDano;

    public float VelYInicial = 0f;
    public float VelXInicialIntervalo = 3f;
    public float Duracao = 4f;

    public void Damage(GameObject target, int dano)
    {
        if (target.CompareTag("npcEnemy"))
        {
            Transform targetTransform = target.transform;
            // Increase the Y scale by 1
            Vector3 currentScale = targetTransform.localScale;
            currentScale.y += 1.0f;
            targetTransform.localScale = currentScale;
        }
        if (target.CompareTag("enemy"))
            Debug.Log("inimigo " + target.name + " machucado");
        {
            target.GetComponent<Cohen>().HP -= dano;
        }
        
        //exibe dano
        canvas = target.transform.GetChild(0).GetComponent<Rigidbody2D>();
        
        valorDano = target.GetComponentInChildren<TMP_Text>();

        valorDano.text = dano.ToString();

        canvas.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
        canvas.gravityScale = 0.5f;
        Destroy(canvas, Duracao);

        //canvas.AddComponent<Rigidbody2D>();

    }
}