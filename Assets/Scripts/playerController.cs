using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{   
    // Patrick 23.06 --- booleana para interromper a Coroutine Move() quando toma dano
    private bool isKnockback = false;

    // Patrick 15.06 --- Blocos seguráveis
    public GameObject blocoSeguravel;
    private bool segurandoBloco = false;
    private bool podePegar = false;
    //

    // Patrick 17.06 --- SFX da Espada
    [Header("SFX de Ataque")]
    public AudioSource espadaSFX;
    public AudioClip sfxClip;

    public void TocaSwingSFX(){
        espadaSFX.pitch = (Random.Range(0.7f, 2.5f));
        espadaSFX.PlayOneShot(sfxClip);
    }
    //

    [Header("Movimento")]
    public float moveSpeed;
    public Rigidbody2D playerRb;
    private bool isMoving = false;
    private bool isAttacking = false; // Para controlar o estado de ataque.
    private Vector2 moveDirection;
    

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
    private BoxCollider2D playerCollider;
    private RaycastHit2D hit;

    /*public LayerMask corposSolidosLayer;
    public LayerMask npcLayer;
    public LayerMask destrutiveisLayer;
    public LayerMask blocoEmpurravelLayer;*/

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        //damageHandler = GetComponent<DamageHandler>();
    }

    public void HandleUpdate() // Tudo relacionado a inputs
    {
        //1. Verifica se o jogador está pressionando alguma tecla OU já está atacando.
        if (!isMoving && !isAttacking)
        { //Move-se apenas se estiver parado e não estiver atacando.
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");

            if (moveDirection.x != 0) moveDirection.y = 0; 
            if (moveDirection != Vector2.zero)
            {
                //Animação
                animator.SetBool("isMoving", true);
                animator.SetFloat(moveXHash, moveDirection.x); // Recomendação da Unity.
                animator.SetFloat(moveYHash, moveDirection.y);

                moveDirection = Vector2.ClampMagnitude(moveDirection, 1f); // Limita a velocidade

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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            isAttacking = true;
            animator.SetTrigger("Atacando");
            TocaSwingSFX();
        }
        // Patrick 15.06 --- Botão de segurar (Z)
        if(Input.GetKeyDown(KeyCode.Z)){
            if(segurandoBloco)
                SoltaBloco();
            else if (podePegar)
                SeguraBloco();
        }
    }
    private void FixedUpdate(){ // Tudo relacionado à movimentação do Rigidbody2D
        isMoving = true;
        
        // Calcula a posição alvo do movimento
        Vector2 targetPosition = playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        
        // Verifica colisões nas camadas NPC e CorposSolidos
        float playerWidth = playerCollider.bounds.size.x;
        float playerHeight = playerCollider.bounds.size.y;
        float castDistance = moveSpeed * Time.fixedDeltaTime + Mathf.Max(playerWidth, playerHeight);
        RaycastHit2D hit = Physics2D.BoxCast(playerRb.position, playerCollider.bounds.size, 0f, moveDirection, castDistance, LayerMask.GetMask("CorposSolidos", "NPC"));
        
        // Desenha o raio na Scene
        Debug.DrawRay(playerRb.position, moveDirection * castDistance, Color.red);

        if(hit.collider == null){ // Não há colisão, pode mover o jogador
            playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        isMoving = false;
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

    // Patrick 22.06 --- Andar em cima do item para coletá-lo
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("coletavel")){
            IColetavel coletavel = collision.GetComponent<IColetavel>();
            if(coletavel != null){
                coletavel.Collect();
            }
            Debug.Log(coletavel);
        }
    }

    public void Interacao()
    {
        var orientacaoJogador = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoInteracao = transform.position + orientacaoJogador;

        Debug.DrawLine(transform.position, posicaoInteracao, Color.red, 1f);

        /*var collider = Physics2D.OverlapCircle(posicaoInteracao, 0.2f, npcLayer); // Checa se, ao fim da linha vermelha (posicaoInteracao) há um NPC.

        if (collider != null)
        {
            Debug.Log("interagiu com o npc: " + collider);
            collider.GetComponent<iInteragivel>()?.Interacao(); // ? significa: Se é interagível, execute a função.
        }
        Debug.Log("não está interagindo com a layer NPC");
        */
    }

    IEnumerator Move(Vector3 targetPos)
    { //Coroutine para mover o sprite.
        isMoving = true;
        
        //enquanto a posicao nao for igual à targetPos...
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        { //Garante que o while seja executado contanto que haja QUALQUER movimento (Epsilon lida com valores muito pequenos)
            if(!isKnockback){
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        while(isKnockback){
            yield return new WaitForSeconds(1f);
            isKnockback = false;
        }

        //transform.position = targetPos;
        isMoving = false;
    }
    public void StopMoveCoroutine(){
        isKnockback = true;
    }
}
