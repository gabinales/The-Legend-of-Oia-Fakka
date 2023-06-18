using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaHit : MonoBehaviour
{
    private DamageHandler damageHandler;
    public float investida;
    public float duracaoDoKnockback;
    public int danoAtaque;

    // SFX do hit Espada
    public AudioSource AudioSource;
    public AudioClip espadaHit;

    private void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    //porrada da espada
    void OnTriggerEnter2D(Collider2D colisor)
    {
        GameObject other = colisor.gameObject;
        //TocaAudioSource();

        // KNOCBACK: (apenas inimigos)
        if (other.CompareTag("enemy"))
        {
            Rigidbody2D adversario = other.GetComponent<Rigidbody2D>();
            Vector2 diferenca = adversario.transform.position - transform.position;
            diferenca = diferenca.normalized * investida;
            adversario.AddForce(diferenca, ForceMode2D.Impulse);
            StartCoroutine(KnockbackCo(adversario));

            damageHandler.Damage(danoAtaque, other);
        }

        if (other.CompareTag("destrutivel"))
        {
            Animator animator = other.GetComponent<Animator>();
            animator.SetBool("cortada", true);
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

    public void TocaAudioSource()
    {
        AudioSource.clip = espadaHit;
        AudioSource.pitch = (Random.Range(1.5f, 1.8f));
        AudioSource.Play();
    }
}
