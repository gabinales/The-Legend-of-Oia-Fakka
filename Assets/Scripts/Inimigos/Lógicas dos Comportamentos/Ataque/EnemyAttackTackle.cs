using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ataque-Tackle", menuName = "Lógica do Inimigo/Ataque/Tackle")]
public class EnemyAttackTackle : EnemyAttackSOBase
{
    [SerializeField] private float _timeBetweenTackles = 2f;
    [SerializeField] private float _timeTillExit = 3f;
    [SerializeField] private float _distanceToCountExit = 3f;

    private float _timer;
    private float _exitTimer;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType){
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic(){
        base.DoEnterLogic();
    }

    public override void DoExitLogic(){
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic(){
        base.DoFrameUpdateLogic();

        enemy.MoveEnemy(Vector2.zero);

        if(_timer > _timeBetweenTackles){
            _timer = 0f;

            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized; // Calcula posição do player
            
            Debug.Log("Atacou!");

            if(!enemy.IsWithingStrikingDistance){
                _exitTimer += Time.deltaTime;
                if(_exitTimer > _timeTillExit){
                    enemy.StateMachine.ChangeState(enemy.ChaseState);
                }
            }
            else{
                _exitTimer = 0f;
            }
        }

        _timer += Time.deltaTime;
    }

    public override void DoPhysicsLogic(){
        base.DoPhysicsLogic();
    }
    public override void Initialize(GameObject gameObject, Enemy enemy){
        base.Initialize(gameObject, enemy);
    }
    
    public override void ResetValues(){
        base.ResetValues();
    }
}
