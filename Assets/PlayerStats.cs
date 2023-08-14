using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Arma{
    Nenhuma,
    Grassblade
}

// Escudo 
// ...
// Bota
// ...

public class PlayerStats : MonoBehaviour, IAtacavel
{   
    private int hpAtual;
    private int hpMax = 10;
    private Arma armaAtual = Arma.Nenhuma;
    private Animator animator;

    public int HpAtual{
        get{
            return hpAtual;
        }
        set{
            hpAtual = value;
            OnHpAlterado(hpAtual); // Aciona o evento e passa o parâmetro para os Listeners interessados (como o UIManager)
        }
    }
    public int HpMax{
        get{
            return hpMax;
        }
        set{
            hpMax = value;
        }
    }
    public Arma ArmaAtual{
        get{
            return armaAtual;
        }
        set{
            armaAtual = value;
            animator.SetBool("Desarmado", armaAtual == Arma.Nenhuma); // Sempre que a propriedade Arma for alterada, o parâmetro "Desarmado" do Animator será atualizado para true quando a arma atual for "Nenhuma" e false para qualquer outro valor.
        }
    }

    public event System.Action<int> HpAlterado; // Torna este evento público para qualquer classe que tenha uma referência de PlayerStats

    public void InitDefaults(){
        HpAtual = HpMax;
        ArmaAtual = Arma.Grassblade;
    }

    private void Awake(){
        animator = GetComponent<Animator>();
        InitDefaults();
    }

    public void Dano(int quantidade){
        HpAtual = hpAtual - quantidade;
    }
    public void Morre(){
        Debug.Log("Morreu");
    }
    
    // permite que subclasses de PlayerStats personalizem o comportamento quando o HP é alterado e acionem o evento HpAlterado, notificando os listeners sobre a mudança no HP.
    protected virtual void OnHpAlterado(int novoHp){
        HpAlterado?.Invoke(novoHp); // Isso notificará todas as classes inscritas no evento, passando o novo valor do HP como parâmetro
    }
}
