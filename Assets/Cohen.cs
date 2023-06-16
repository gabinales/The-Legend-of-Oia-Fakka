using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohen : AdversarioScript // Cohen contém todas as funções de Adversario.
{
    public Transform alvo; // Posição do jogador
    public Transform posicaoInicial;
    public float areaDePerseguicao;
    public float areaDoAtaque;
    public Animator animator;

    void Start()
    {
        alvo = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if (gameObject.GetComponent<Cohen>().HP<=0) animator.SetBool("Vivo", false);
        VerificaDistancia();
    }

    void VerificaDistancia()
    {
        if (Vector3.Distance(alvo.position, transform.position) <= areaDePerseguicao
                                                            && Vector3.Distance(alvo.position, transform.position) > areaDoAtaque)
        { // O inimigo precisa parar de perseguir o jogador assim que entra na área de ataque, se não ele continua tentando ocupar o espaço do jogador.
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
            animator.SetBool("Perseguindo", true);
        }
        else animator.SetBool("Perseguindo", false);
    }

    void Morre(){
        Destroy(gameObject);
    }
}
