using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IAtacavel
{
    private int hp;
    private int hpMax = 10;

    public int Hp{
        get{
            return hp;
        }
        set{
            hp = value;
            OnHpAlterado(hp); // Aciona o evento e passa o parâmetro para os Listeners (como o UIManager)
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

    public event System.Action<int> HpAlterado; 

    public void InitDefaults(){
        Hp = HpMax;
    }

    private void Awake(){
        InitDefaults();
    }

    public void Dano(int quantidade){
        Debug.Log("Dano");
    }
    public void Cura(int quantidade){
        Debug.Log("Cura");
    }
    
    // permite que subclasses de PlayerStats personalizem o comportamento quando o HP é alterado e acionem o evento HpAlterado, notificando os listeners sobre a mudança no HP.
    protected virtual void OnHpAlterado(int novoHp){
        HpAlterado?.Invoke(novoHp);
    }

}
