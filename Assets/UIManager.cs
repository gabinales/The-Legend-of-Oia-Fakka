using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] private TMP_Text hpText;

    private void OnEnable(){ // Executado quando o componente é habilitado
        playerStats.HpAlterado += AtualizaHpText; // inscreve o método AtualizaHpText, que será executado somente quando que o evento HpAlterado for acionado
    }
    private void OnDisable(){ // Executado quando o componente é desabilitado
        playerStats.HpAlterado -= AtualizaHpText; // desinscreve o método
    }
    
    void Start(){
        AtualizaHpText(playerStats.Hp);
    } 
    private void AtualizaHpText(int novoHp){
        hpText.text = "HP: " + novoHp.ToString();
    }
}
