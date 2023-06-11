using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    //Animação do sprite
    private Animator animator;
    private int moveXHash = Animator.StringToHash("moveX");
    private int moveYHash = Animator.StringToHash("moveY");
    
    
    //Detecção de colisão do sprite utilizando a layer
    public LayerMask corposSolidosLayer;
    public LayerMask npcLayer;
    public LayerMask destrutiveisLayer;

    //Ataque
    //private bool atacando = false;
    //private int atacandoHash = Animator.StringToHash("Atacando");
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate(){
        //1. Verifica se o jogador está pressionando alguma tecla. Se não, executa o bloco correspondente à posição IDLE
        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x != 0) input.y = 0; // Garante que o sprite movimente-se apenas em 4 direções.

            if(input != Vector2.zero){
                //Animação
                animator.SetFloat(moveXHash, input.x); //Essa parada de Hash é recomendação da Unity.
                animator.SetFloat(moveYHash, input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                //Antes de executar a movimentação, verifica se o alvo é caminhável (detecta colisão)
                if(IsWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));
                }   
            }

            //Botão de Interação (C)
            if(Input.GetKeyDown(KeyCode.C)){
                Interacao();
            }
            //Botão de Ataque (X)
            if(Input.GetKeyDown(KeyCode.X)){
                if(!isMoving){
                //if(!isMoving && !Atacando){
                    animator.SetTrigger("Atacando");
                    Ataca();
                }
            }
            
        }
        //2. Se está se movendo, executa a animação correspondente (CORRE)
        animator.SetBool("isMoving", isMoving);
        
    }
    public void Interacao(){
        var orientacaoJogador = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoInteracao = transform.position + orientacaoJogador;

        Debug.DrawLine(transform.position, posicaoInteracao, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(posicaoInteracao, 0.2f, npcLayer); // Checa se, ao fim da linha vermelha (posicaoInteracao) há um NPC.
        Debug.Log(collider);
        
        if(collider != null){
            // Checa se o NPC alvo da interação é inimigo, personagem etc.
            collider.GetComponent<NPC>()?.Interacao(); // Se é interagível, execute a função.
        }
    }

    public void Ataca(){
        var orientacaoJogador = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoAtaque = (Vector2)transform.position;

        RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2)transform.position, orientacaoJogador, LayerMask.GetMask("Destrutiveis"));

        if (raycastHit2D){
            Debug.Log(raycastHit2D.collider.gameObject.name);
            Debug.DrawRay((Vector2)transform.position, orientacaoJogador, Color.blue, 2f);
        }
               // GameObject hitObject = hit.collider.gameObject;

    }


    IEnumerator Move(Vector3 targetPos){ //Coroutine para mover o sprite.
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){ //Garante que o while seja executado contanto que haja QUALQUER movimento (Epsilon lida com valores muito pequenos)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.2f, corposSolidosLayer | npcLayer) != null){ // Se o jogador tentar colidir com um CORPO SÓLIDO ou NPC, então NÃO ANDE.
            
            return false;
        }
        return true;
    }

    
}
