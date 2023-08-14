using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAtacavel, IInimigoMovivel, ITriggerChecavel
{
    [Header("Geral")]
    public PlayerStats player;
    public int defesa;
    public int danoBase; // descomentei
    public int danoToque;
    public string nomeAdversario;
    public float velocidade;
    public Animator animator;

    [field: SerializeField] public int HpMax{ get; set; }
    public int HpAtual{ get; set; }

    public Rigidbody2D rb { get; set; }
    public bool IsFacingRight { get; set;} = true;

    public bool IsAggroed { get; set; }
    public bool IsWithingStrikingDistance { get; set; }

    // Variáveis dos SO
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; } //tem que instanciar pq cada inimigo age individualmente

    // Variáveis da state machine
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }


    private void Awake(){
        // As instâncias dos SO são puxadas aqui:
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);


        StateMachine = new EnemyStateMachine();
        
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine); 
    }
    private void Start(){
        HpAtual = HpMax;
        rb = GetComponent<Rigidbody2D>();

        // Inicializa as variáveis dos SOs:
        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        // Inicializa a state machine:
        StateMachine.Initialize(IdleState);
    }

    private void Update(){
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate(){
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    // Aggro
    public void SetAggroStatus(bool isAggroed){
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithingStrikingDistance){
        IsWithingStrikingDistance = isWithingStrikingDistance;
    }



    public void Dano(int quantidade){
        HpAtual -= quantidade;
        if(HpAtual <= 0){
            Morre();
        }
    }
    public void Morre(){
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocidade){
        rb.velocity = velocidade;
        CheckForLeftOrRightFacing(velocidade);
    }
    public void CheckForLeftOrRightFacing(Vector2 velocidade){
        if(IsFacingRight && velocidade.x < 0f){
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if(!IsFacingRight && velocidade.x > 0f){
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType){
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType{
        EnemyDamaged
    }


    private void OnCollisionEnter2D(Collision2D colisor){
        if(colisor.gameObject.CompareTag("Player")){
            IAtacavel IAtacavel = colisor.gameObject.GetComponent<IAtacavel>();
            if(IAtacavel != null){
                IAtacavel.Dano(danoToque);
                cameraController.instance.ScreenKick();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.danoHit, this.transform.position);
            }
        }
    }
}
