using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] private TMP_Text hpText;

    private void OnEnable(){
        playerStats.HpAlterado += AtualizaHpText;
    }
    private void OnDisable(){
        playerStats.HpAlterado -= AtualizaHpText;
    }
    
    void Start(){
        AtualizaHpText(playerStats.Hp);
    } 
    private void AtualizaHpText(int novoHp){
        hpText.text = "HP: " + novoHp.ToString();
    }
}
