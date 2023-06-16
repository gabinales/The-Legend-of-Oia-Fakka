using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private DamageHandler damageHandler;
    public float investida;
    public float duracaoDoKnockback;

    private void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("enemy"))
            {
                damageHandler.Damage(other.gameObject);

                Rigidbody2D adversario = other.GetComponent<Rigidbody2D>();
                if (adversario != null)
                {
                    adversario.isKinematic = false;
                    Vector2 diferenca = adversario.transform.position - transform.position;
                    diferenca = diferenca.normalized * investida;
                    adversario.AddForce(diferenca, ForceMode2D.Impulse);
                    StartCoroutine(KnockbackCo(adversario));
                }
            }
    }
    private IEnumerator KnockbackCo(Rigidbody2D adversario)
    {
        if (adversario != null)
        {
            yield return new WaitForSeconds(duracaoDoKnockback);
            adversario.velocity = Vector2.zero;
            adversario.isKinematic = true;
        }
    }
}
