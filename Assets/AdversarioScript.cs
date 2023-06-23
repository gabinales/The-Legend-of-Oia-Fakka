using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversarioScript : MonoBehaviour
{
    public int hpMax;
    public int hpAtual;
    public int defesa;
    public int danoBase; // descomentei
    public int danoToque;
    public string nomeAdversario;
    public float velocidade;
    public AudioSource audiosource;
    public List<AudioClip> enemySfx;
    public Animator animator;

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
}
