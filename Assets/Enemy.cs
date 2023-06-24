using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAtacavel
{
    public PlayerStats player;
    //public playerController pController;
    public int hpMax;
    public int hpAtual;
    public int defesa;
    public int danoBase; // descomentei
    public int danoToque;
    public float investidaKnockback;
    public float duracaoKnockback;
    public string nomeAdversario;
    public float velocidade;
    public AudioSource audiosource;
    public List<AudioClip> enemySfx;
    public Animator animator;

    private bool isKnockback = false;

    private void Awake(){
        //pController = FindObjectOfType<playerController>();
    }

    public void SfxDamaged(){
        if(hpAtual >= 0){
            if(!audiosource.isPlaying){
                audiosource.PlayOneShot(enemySfx[1]);
            }
        }
        Debug.Log("Hp atual: "+hpAtual);
        Debug.Log("Som tocado: "+ enemySfx[1]);
    }
    public void SfxDefeated(){
        if(!audiosource.isPlaying){
            audiosource.PlayOneShot(enemySfx[2]);
        }
        animator.SetBool("Morte", true);
    }

    public void Dano(int quantidade){
        player.Hp = player.Hp - quantidade;
    }
    public void Cura(int quantidade){
        Debug.Log("Cura");
    }

    private void OnCollisionEnter2D(Collision2D colisor){
        if(colisor.gameObject.CompareTag("Player")){
            Dano(danoToque);

            Rigidbody2D other = colisor.gameObject.GetComponent<Rigidbody2D>();
            if(other != null){
                StartCoroutine(KnockbackCo(other));
            }
        }
    }

    private IEnumerator KnockbackCo(Rigidbody2D other){
        //isKnockback = true;

        Vector2 direcaoKnockback = other.transform.position - transform.position;
        direcaoKnockback = direcaoKnockback.normalized;
        //other.velocity = Vector2.zero;
        other.AddForce(direcaoKnockback * investidaKnockback, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duracaoKnockback);

        other.velocity = Vector2.zero;
        //isKnockback = false;
    }
}
