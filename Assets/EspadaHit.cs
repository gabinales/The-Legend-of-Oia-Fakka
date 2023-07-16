using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EspadaHit : MonoBehaviour
{
    private DamageHandler damageHandler;
    public float investida;
    public float duracaoDoKnockback;
    public int danoAtaque;

    // VFX do hit da Espada
    public GameObject Fagulhas;

    private void Awake()
    {
        damageHandler = GetComponent<DamageHandler>();
    }

    //porrada da espada
    void OnTriggerEnter2D(Collider2D colisor)
    {
        GameObject other = colisor.gameObject;

        // Dano e Knockback: (apenas inimigos)
        if (other.CompareTag("enemy"))
        {
            Animator advAnimator = other.GetComponent<Animator>();
            advAnimator.SetTrigger("Damaged");

            Rigidbody2D adversario = other.GetComponent<Rigidbody2D>();
            Vector2 diferenca = adversario.transform.position - transform.position;
            diferenca = diferenca.normalized * investida;
            adversario.AddForce(diferenca, ForceMode2D.Impulse);
            StartCoroutine(KnockbackCo(adversario));

            damageHandler.Damage(danoAtaque, other);
            //danoAtaque *= 2;
        }
        // Outros efeitos (cortar grama, ativar switch...):
        if (other.CompareTag("destrutivel"))
        {
            tileDestrutivel obj = other.GetComponent<tileDestrutivel>();
            obj.TocaSFX();
            Animator animator = other.GetComponent<Animator>();
            animator.SetBool("destroy", true);
        }
        if(other.CompareTag("switch")){
            ISwitch switchObj = other.GetComponent<ISwitch>();
            if(switchObj != null){
                switchObj.Toggle();
            }
        }
        // Patrick -- 21.06 Efeitos de partícula
        if(other.CompareTag("barreira")){
            ParticleSystem fagulhas = GetComponent<ParticleSystem>();
            fagulhas.Play();
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
