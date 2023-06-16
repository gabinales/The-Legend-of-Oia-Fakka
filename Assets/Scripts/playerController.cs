using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private DamageHandler damageHandler;

    // Patrick 15.06 --- Blocos seguráveis
    public GameObject blocoSeguravel;
    private bool segurandoBloco = false;
    private bool podePegar = false;
    //

    [Header("Movimento")]
    public float moveSpeed;
    private bool isMoving = false;
    private bool isAttacking = false; // Para controlar o estado de ataque.
    private Vector2 input;

    //Animação do sprite
    private Animator animator;
    private int moveXHash = Animator.StringToHash("moveX");
    private int moveYHash = Animator.StringToHash("moveY");

    public void podeMover()
    {
        animator.ResetTrigger("Atacando");
        isAttacking = false;
    }

    //Detecção de colisão do sprite utilizando a layer
    public LayerMask corposSolidosLayer;
    public LayerMask npcLayer;
    public LayerMask destrutiveisLayer;
    public LayerMask blocoEmpurravelLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageHandler = GetComponent<DamageHandler>();

    }

    public void HandleUpdate()
    {
        //1. Verifica se o jogador está pressionando alguma tecla OU já está atacando.
        if (!isMoving && !isAttacking)
        { //Move-se apenas se estiver parado e não estiver atacando.
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // Garante que o sprite movimente-se apenas em 4 direções.

            if (input != Vector2.zero)
            {
                //Animação
                animator.SetBool("isMoving", true);
                animator.SetFloat(moveXHash, input.x); // Recomendação da Unity.
                animator.SetFloat(moveYHash, input.y);

                var targetPos = transform.position;
                targetPos.x += input.x / 4;
                targetPos.y += input.y / 4;

                if(IsWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));
                }
                
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        //Botão de Interação (C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            Interacao();
        }
        //Botão de Ataque (X)
        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttacking = true;
            animator.SetTrigger("Atacando");
            Ataque();
        }
        // Patrick 15.06 --- Botão de segurar (Z)
        if(Input.GetKeyDown(KeyCode.Z)){
            if(segurandoBloco)
                SoltaBloco();
            else if (podePegar)
                SeguraBloco();
        }

    }
    // Patrick 15.06 --- Botão de segurar (Z)
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject == blocoSeguravel)
            podePegar = true;
    }
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject == blocoSeguravel)
            podePegar = false;
    }

    void SeguraBloco(){
        blocoSeguravel.transform.SetParent(transform);
        segurandoBloco = true;
    }
    void SoltaBloco(){
        blocoSeguravel.transform.SetParent(null);
        segurandoBloco = false;
    }

    public void Interacao()
    {
        var orientacaoJogador = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoInteracao = transform.position + orientacaoJogador;

        Debug.DrawLine(transform.position, posicaoInteracao, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(posicaoInteracao, 0.2f, npcLayer); // Checa se, ao fim da linha vermelha (posicaoInteracao) há um NPC.
        Debug.Log(collider);

        if (collider != null)
        {
            // Checa se o NPC alvo da interação é inimigo, personagem etc.
            collider.GetComponent<NPC>()?.Interacao(); // Se é interagível, execute a função.
        }
    }

    public void Ataque()
    {
        Debug.Log("Apertou X para atacar [mas ainda não existe nenhuma função para isso]");

        var orientacaoJogador = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        int targetLayer = LayerMask.GetMask("Destrutiveis");
        float raycastDistance = 1f;

        //o target apenas é detectado na "targetLayer" "Destrutiveis"
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, orientacaoJogador, raycastDistance, targetLayer);

        Debug.DrawRay(transform.position, orientacaoJogador, Color.blue, raycastDistance);

        //se detectou um target, faz um debug log. além dissose estiver armado, chama a função "Damage" do script DamageHandler.cs
        if (raycastHit2D.collider != null)
        {
            GameObject objectHit = raycastHit2D.collider.gameObject;
            Debug.Log(objectHit);

            bool desarmado = animator.GetBool("Desarmado");
            if (!desarmado)
            {
                damageHandler.Damage(objectHit);
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    { //Coroutine para mover o sprite.
        isMoving = true;
        //enquanto a posicao nao for igual à targetPos...
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        { //Garante que o while seja executado contanto que haja QUALQUER movimento (Epsilon lida com valores muito pequenos)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Collider2D colisor = Physics2D.OverlapCircle(targetPos, 0.1f, corposSolidosLayer | npcLayer);
        if (colisor != null)
        { // Se o jogador tentar colidir com um CORPO SÓLIDO ou NPC, então NÃO ANDE.
            Debug.Log(colisor.gameObject.layer);
            Vector3 direction = targetPos - transform.position;
            direction.Normalize();
        
            return false;
        }
        return true;
    }
}




/*
Collider2D colisor = Physics2D.OverlapCircle(targetPos, 0.2f, corposSolidosLayer | npcLayer | blocoEmpurravelLayer);
        if (colisor != null)
        { // Se o jogador tentar colidir com um CORPO SÓLIDO ou NPC, então NÃO ANDE.
            Debug.Log(colisor.gameObject.layer);
            Vector3 direction = targetPos - transform.position;
            direction.Normalize();
            Collider2D bloqueioAtras = Physics2D.OverlapCircle(colisor.gameObject.transform.position + direction, 0.2f, corposSolidosLayer | npcLayer | blocoEmpurravelLayer);
            Debug.Log(bloqueioAtras);
            Debug.DrawRay(targetPos, direction, Color.red, 0.4f);
            if(colisor.gameObject.layer == 10 && bloqueioAtras == null){ //10 = blocoEmpurravel
                colisor.gameObject.transform.position = colisor.gameObject.transform.position + direction;
                return false;
            }
            else{
                return false;
            }
        }
        return true;

        */