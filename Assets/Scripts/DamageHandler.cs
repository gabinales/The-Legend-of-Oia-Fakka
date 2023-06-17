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
    }

    // Valor do dano saltando na tela.
    private Rigidbody2D rbTexto; // O Canvas pai do Texto contém um Rigidbody2D
    private TMP_Text valorDano;

    public float VelYInicial = 7f;
    public float VelXInicialIntervalo = 3f;
    public float Duracao = 0.8f;

    public void ExibeTexto(GameObject alvo){
        rbTexto = alvo.GetComponentInChildren<Rigidbody2D>();
        valorDano = alvo.GetComponentInChildren<TMP_Text>();
        rbTexto.velocity = new Vector2(Random.Range(-VelXInicialIntervalo, VelXInicialIntervalo), VelYInicial);
        Destroy(gameObject, Duracao);        
    }
    // ---

    public void Damage(int amount) => Hp -= amount; // adicionei esse int amount -- Patrick
    
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
    public void Kill() => Hp = _maxHp;
    public void Adjust(int value) => Hp = value;
}