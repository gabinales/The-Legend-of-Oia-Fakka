using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
    // Patrick 16.06 --- Gerenciador de Dano Universal
    [SerializeField]
    private int _maxHp = 100;
    private int _hp; // HP atual

    public int MaxHp => _maxHp;  // Outros recursos podem utilizar este valor, como barra de vida.

    // Tiles destrutíveis (grama, potes, etc.)
    private Animator animator;
    //

    public int Hp{
        get => _hp;
        private set{
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, 0, _maxHp);
            if(isDamage){
                Damaged?.Invoke(_hp);
            }
            else{
                Healed?.Invoke(_hp);
            }
            if(_hp <= 0){
                Died?.Invoke();
            }
        }
    }

    public UnityEvent<int> Healed; // Exige using UnityEngine.Events;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    private void Awake(){
        _hp = _maxHp;

        animator = GetComponent<Animator>();
    }

    // Valor do dano saltando na tela.
    private Rigidbody2D canvas;
    private TMP_Text valorDano;

    public float VelYInicial = 0f;
    public float VelXInicialIntervalo = 3f;
    public float Duracao = 1f;

    public void Damage(int amount){
        Hp -= amount;

        if(!gameObject.CompareTag("destrutivel")){
            // Damage pop-up
            canvas = gameObject.transform.GetChild(0).GetComponentInChildren<Rigidbody2D>();
            valorDano = gameObject.GetComponentInChildren<TMP_Text>();
            valorDano.text = amount.ToString();
            canvas.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
            canvas.gravityScale = 0.5f;
            Destroy(canvas, Duracao);
        }else if(gameObject.CompareTag("destrutivel")){
            tileDestrutivel(animator);
        }
    }
    
        /*if (target.CompareTag("npcEnemy"))
        {
            Transform targetTransform = target.transform;
            // Increase the Y scale by 1
            Vector3 currentScale = targetTransform.localScale;
            currentScale.y += 1.0f;
            targetTransform.localScale = currentScale;
        }*/
    public void Heal(int amount) => Hp += amount;
    /*Mesma coisa que:
    public void Heal(int amount){
        HP += amount;
    }*/
    public void HealFull() => Hp = _maxHp;
    public void Kill() => Destroy(gameObject);
    public void Adjust(int value) => Hp = value;


    public void tileDestrutivel(Animator animator){
        animator.SetBool("cortada", true);
    }
}