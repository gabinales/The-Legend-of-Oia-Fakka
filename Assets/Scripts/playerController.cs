using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class playerController : MonoBehaviour
{
    // Patrick 23.06 --- booleana para interromper a Coroutine Move() quando toma dano
    private bool isKnockback = false;

    // Patrick 15.06 --- Blocos seguráveis
    public GameObject blocoSeguravel;
    private bool segurandoBloco = false;
    private bool podeSegurar = false;
    //

    // Patrick 17.06 --- SFX da Espada
    [Header("SFX de Ataque")]
    public AudioSource espadaSFX;
    public AudioClip sfxClip;

    public void TocaSwingSFX()
    {
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

    public LayerMask corposSolidosLayer;
    public LayerMask npcLayer;
    public LayerMask destrutiveisLayer;
    public LayerMask blocoEmpurravelLayer;

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
            Interage();
        }
        //Botão de Ataque (X)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            if (!isMoving)
            {
                isAttacking = true;
                animator.SetTrigger("Atacando");
                TocaSwingSFX();
            }

        }
        // Patrick 15.06 --- Botão de segurar (Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (segurandoBloco)
                SoltaBloco();
            else if (podeSegurar)
                SeguraBloco();
        }
    }
    private void FixedUpdate()
    { // Tudo relacionado à movimentação do Rigidbody2D

        // Calcula a posição alvo do movimento
        Vector2 targetPosition = playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // Verifica colisões nas camadas NPC e CorposSolidos
        float playerWidth = playerCollider.bounds.size.x;
        float playerHeight = playerCollider.bounds.size.y;
        float castDistance = moveSpeed * Time.fixedDeltaTime + Mathf.Max(playerWidth, playerHeight);
        RaycastHit2D hit = Physics2D.BoxCast(playerRb.position, playerCollider.bounds.size, 0f, moveDirection, castDistance / 4, LayerMask.GetMask("CorposSolidos", "NPC"));

        // Desenha o raio na Scene
        Debug.DrawRay(playerRb.position, moveDirection * castDistance, Color.red);

        if (hit.collider == null && !isAttacking)
        { // Não há colisão, pode mover o jogador
            playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            isMoving = true;
        }
        isMoving = false;
    }

    // Patrick 15.06 --- Botão de segurar (Z)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == blocoSeguravel)
            podeSegurar = true;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == blocoSeguravel)
            podeSegurar = false;
    }

    void SeguraBloco()
    {
        blocoSeguravel.transform.SetParent(transform);
        segurandoBloco = true;
    }
    void SoltaBloco()
    {
        blocoSeguravel.transform.SetParent(null);
        segurandoBloco = false;
    }

    // Patrick 22.06 --- Andar em cima do item para coletá-lo
    // Patrick 28.06 --- Colisão com os triggers de cutscenes
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("coletavel"))
        {
            IColetavel coletavel = collision.GetComponent<IColetavel>();
            if(coletavel != null && Input.GetKeyDown(KeyCode.V)){
                coletavel.Collect();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("cutscene")){
            CutsceneTrigger cutsceneTrigger = collision.GetComponentInParent<CutsceneTrigger>();
            if(cutsceneTrigger != null){
                Transform playerTransform = transform;
                Vector2 closestPoint = collision.ClosestPoint(playerTransform.position); // Retorna o ponto no perímetro deste Collider mais próximo à posição especificada.

                // Verificar qual filho possui posição mais próxima ao ponto de colisão
                Transform collidedTrigger = GetClosestTriggerTransform(closestPoint, cutsceneTrigger.transform);
                if(collidedTrigger != null){
                    cutsceneTrigger.StartCutscene(collidedTrigger);
                }
            }
        }
    }

    private Transform GetClosestTriggerTransform(Vector2 point, Transform parent){ // Retorna o Transform do filho mais próximo em relação a um ponto específico.
        Transform closestChild = null;
        float closestDistance = Mathf.Infinity; // Ao iniciar a variável com Mathf.Infinity, garante-se que qualquer valor real de distância seja menor do que closestDistance, permitindo que seja substituído no decorrer do código.

        foreach(Transform child in parent){
            float distance = Vector2.Distance(child.position, point);
            if(distance < closestDistance){
                closestChild = child;
                closestDistance = distance;
            }
        }
        return closestChild;
    }
    // 

    public void Interage()
    {
        var orientacaoJogador = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoInteracao = transform.position + orientacaoJogador;

        Debug.DrawLine(transform.position, posicaoInteracao, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(posicaoInteracao, 0.2f, npcLayer); // Checa se, ao fim da linha vermelha (posicaoInteracao) há um NPC.

        if (collider != null)
        {
            //Debug.Log("interagiu com o npc: " + collider);
            collider.GetComponent<iInteragivel>()?.Interacao(); // ? significa: Se é interagível, execute a função.
        }
        Debug.Log("não está interagindo com a layer NPC");

    }
}
