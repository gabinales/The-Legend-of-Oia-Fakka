using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuracoDano : MonoBehaviour
{
    private Vector2 direcaoUltimoMovimento;
    public playerController playerControllerScript; // Referência para o script playerController
    private PlayerStats pStats;
    private SalvarChaoSeguro safeGroundSaver;

    private void Start(){
        pStats = FindObjectOfType<PlayerStats>();
        safeGroundSaver = FindObjectOfType<SalvarChaoSeguro>();
        // safeGroundSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<SalvarChaoSeguro>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Salva a direção do último movimento do jogador
                direcaoUltimoMovimento = playerRb.velocity.normalized;

                // Desativa temporariamente o script playerController
                playerControllerScript.enabled = false;

                // Ajusta a posição do jogador
                Vector2 novaPosicao = (Vector2)other.transform.position + direcaoUltimoMovimento * 1f;
                other.transform.position = novaPosicao;

                // Ativa o trigger "Caindo" no Animator do jogador
                other.GetComponent<Animator>().SetTrigger("Caindo");

                // Inflige o dano da queda
                pStats.Dano(1);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.caiuNoBuraco, this.transform.position);

                safeGroundSaver.WarpPlayerToSafeGround();

                StartCoroutine(RetomarControle(other));
            }
        }
    }

    private IEnumerator RetomarControle(Collider2D other)
    {
        yield return new WaitForSeconds(1f); // Ajuste o tempo de espera conforme necessário
        playerControllerScript.enabled = true; // Reativa o script playerController
    }
}