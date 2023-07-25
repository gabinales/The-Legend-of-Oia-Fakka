using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class playerController : MonoBehaviour
{
    private static playerController instance;
    public static playerController Instance{
        get{
            return instance;
        }
    }
    private FMODEvents fmodEvents;
    private int currentTerrainType;

    private PlayerStats pStats;
    public VetorPosicaoInicialPlayer posicaoInicial; // Útil para reposicionar corretamente o jogador 
                                                        // durante as transições entre Scenes

    public void TocaAtaqueSFX()
    {
        if(pStats.ArmaAtual == Arma.Nenhuma){
            FMODEvents.instance.SetArmaType(ArmaType.Nenhuma);
            FMODEvents.instance.PlaySvardAttackSound();
        }
        else if(pStats.ArmaAtual == Arma.Grassblade){
            FMODEvents.instance.SetArmaType(ArmaType.Grassblade);
            FMODEvents.instance.PlaySvardAttackSound();
        }
        else{
            Debug.Log("TocaAtaqueSFX: Arma inválida!!!");
        }
    }

    [Header("Movimento")]
    public float moveSpeed;
    public Rigidbody2D playerRb;
    private bool isMoving = false;
    private bool isAttacking = false; // Para controlar o estado de ataque.
    public Vector2 moveDirection;

    //Animação do sprite
    public Animator animator;
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

    [Header("Passos SFX")]
    private float distanceMoved = 0f;
    public float stepDistance = 1f;

    private void Start(){
        transform.position = posicaoInicial.posicaoInicial;
    }
    private void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }
        else{
            instance = this;
        }

        playerCollider = GetComponent<BoxCollider2D>();
        pStats = GetComponent<PlayerStats>();
        fmodEvents = FMODEvents.instance;
    }

    public void HandleUpdate() // Tudo relacionado a inputs, executado de acordo com o state MovimentacaoLivre no GameController
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
            if (!isMoving && !isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("Atacando");
                TocaAtaqueSFX();
            }
        }
    }

    public void UpdateTerrainType(int terrainType){
        currentTerrainType = terrainType;
    }
    private void FixedUpdate()
    { // Tudo relacionado à movimentação do Rigidbody2D

        // Calcula a posição alvo do movimento
        Vector2 targetPosition = playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // Verifica colisões nas camadas NPC e CorposSolidos
        float playerWidth = playerCollider.bounds.size.x;
        float playerHeight = playerCollider.bounds.size.y;
        float castDistance = moveSpeed * Time.fixedDeltaTime + Mathf.Max(playerWidth, playerHeight);
        RaycastHit2D hit = Physics2D.BoxCast(playerRb.position, playerCollider.bounds.size, 0f, moveDirection, castDistance / 8, LayerMask.GetMask("CorposSolidos", "NPC"));

        // Desenha o raio na Scene
        Debug.DrawRay(playerRb.position, moveDirection * castDistance/8, Color.red);

        if (hit.collider == null && !isAttacking)
        { // Não há colisão, pode mover o jogador
            float moveDistance = Vector2.Distance(playerRb.position, targetPosition);
            distanceMoved += moveDistance;

            if(distanceMoved >= stepDistance){
                distanceMoved -= stepDistance;
                
                PlayFootstepSound();
            }

            playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            isMoving = true;
        }
        isMoving = false;
    }

    private void PlayFootstepSound(){
        if(fmodEvents != null){
            fmodEvents.SetFootstepTerrain(currentTerrainType);
            fmodEvents.PlayFootstepSound();
        }
    }

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

    public void Interage()
    {
        var orientacaoJogador = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")); //reutilizando as posições que já estão settadas para o Animator.
        var posicaoInteracao = transform.position + orientacaoJogador / 4;

        Debug.DrawLine(transform.position, posicaoInteracao, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(posicaoInteracao, 0.2f, npcLayer | corposSolidosLayer); // Checa se, ao fim da linha vermelha (posicaoInteracao) há um NPC.

        if (collider != null)
        {
            collider.GetComponent<iInteragivel>()?.Interacao(); // ? significa: Se é interagível, execute a função.
        }
        Debug.Log("Não há nenhum NPC ou Corpo Sólido específico ao seu alcance.");
    }
}
