using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaHit : MonoBehaviour
{
    // Gerenciador de Vida Universal -- Patrick

    //private DamageHandler damageHandler;
    public float investida;
    public float duracaoDoKnockback;
    private int amount;
    public int danoAtaque;

    private List<DamageHandler> _objectsWithHealth = new List<DamageHandler>();
    //

    private void Awake()
    {
        //damageHandler = GetComponent<DamageHandler>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<DamageHandler>(out var damageHandler))
        {
            //damageHandler.Damage(danoAtaque);  // <---- Fiz assim
            
            _objectsWithHealth.Add(damageHandler);
        }
        for(int i = _objectsWithHealth.Count - 1; i >= 0; i--){
            _objectsWithHealth[i].Damage(danoAtaque);
        } 
        _objectsWithHealth.Clear();

            //damageHandler.ExibeTexto(other.gameObject);

            // KNOCBACK:
            Rigidbody2D adversario = other.gameObject.GetComponent<Rigidbody2D>(); // NOVA
            //Rigidbody2D adversario = other.GetComponent<Rigidbody2D>(); ANTIGA
            if (adversario != null)
            {
                //adversario.isKinematic = false;
                Vector2 diferenca = adversario.transform.position - transform.position;
                diferenca = diferenca.normalized * investida;
                adversario.AddForce(diferenca, ForceMode2D.Impulse);
                StartCoroutine(KnockbackCo(adversario));
            }
        }
    private IEnumerator KnockbackCo(Rigidbody2D adversario)
    {
        if (adversario != null)
        {
            yield return new WaitForSeconds(duracaoDoKnockback);
            adversario.velocity = Vector2.zero;
            //adversario.isKinematic = true;
        }
    }
}
