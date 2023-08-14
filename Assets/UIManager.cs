using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image playerStatsPanel;

    private float timerInatividade;
    public float thresholdInatividade = 15f;
    Color ativoTransparencia = new Color(1f,1f,1f,0.8f);
    Color inativoTransparencia = new Color(1f,1f,1f,0.5f);

    private GameController gameController;

    private void OnEnable(){ // Executado quando o componente é habilitado
        playerStats.HpAlterado += AtualizaHpText; // inscreve o método AtualizaHpText, que será executado somente quando que o evento HpAlterado for acionado
    }
    private void OnDisable(){ // Executado quando o componente é desabilitado
        playerStats.HpAlterado -= AtualizaHpText; // desinscreve o método
    }
    
    void Start(){
        gameController = FindObjectOfType<GameController>();
        AtualizaHpText(playerStats.HpAtual);
    }

    private void Update(){
        if(gameController == null || gameController.state != GameState.MovimentacaoLivre){
            // Se o jogo não estiver no estado de Movimentação Livre, não rastreie a inatividade
            return;
        }
        if(Input.anyKeyDown || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            timerInatividade = 0f;
            AlteraTransparencia(ativoTransparencia);
        }
        else{
            timerInatividade += Time.deltaTime;

            if(timerInatividade >= thresholdInatividade){
                AlteraTransparencia(inativoTransparencia);
            }
        }
    }
    private void AlteraTransparencia(Color transparencia){
        playerStatsPanel.color = transparencia;
    }

    private void AtualizaHpText(int novoHp){
        hpText.text = "HP: " + novoHp.ToString();
    }
}
