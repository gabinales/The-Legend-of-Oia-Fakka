using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageHandler : MonoBehaviour
{
    // Patrick 16.06 --- Pop-up de dano
    private Rigidbody2D rbTexto; // O Canvas pai do Texto contém um Rigidbody2D
    private TMP_Text valorDano;

    public float VelYInicial = 7f;
    public float VelXInicialIntervalo = 3f;
    public float Duracao = 0.8f;

    private void Awake(){
        rbTexto = GetComponent<Rigidbody2D>();
        valorDano = GetComponentInChildren<TMP_Text>();
    }
    private void Start(){
        rbTexto.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
        Destroy(gameObject, Duracao);
    }
    private void ExibeTexto(string dano){
        valorDano.SetText(dano);
    }
    //

    public void Damage(GameObject target)
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
        Debug.Log("inimigo "+ target.name + " machucado");
        {
            target.GetComponent<Cohen>().HP -= 1;
            
        }

    }
}