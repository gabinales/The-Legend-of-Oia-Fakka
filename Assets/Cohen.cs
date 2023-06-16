using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohen : AdversarioScript // Cohen contém todas as funções de Adversario.
{
    public Transform alvo; // Posição do jogador
    public Transform posicaoInicial;
    public float areaDePerseguicao;
    public float areaDoAtaque; // ???

    void Start(){
        alvo = GameObject.FindWithTag("Player").transform;
    }
    void Update(){
        VerificaDistancia();
    }

    void VerificaDistancia(){
        if(Vector3.Distance(alvo.position, transform.position) <= areaDePerseguicao
                                                            && Vector3.Distance(alvo.position, transform.position) > areaDoAtaque){ // O inimigo precisa parar de perseguir o jogador assim que entra na área de ataque, se não ele continua tentando ocupar o espaço do jogador.
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
        }
    }
}
