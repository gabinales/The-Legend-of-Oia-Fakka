using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Lógica do Inimigo/Idle/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] public float RandomMovementRange = 5f;
    [SerializeField] public float RandomMovementSpeed = 1f;

    private Vector3 _targetPos;
    private Vector3 _direction;
    
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType){
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic(){
        base.DoEnterLogic();

        _targetPos = GetRandomPointInCircle();
    }

    public override void DoExitLogic(){
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic(){
        base.DoFrameUpdateLogic();

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * RandomMovementSpeed);

        // Quando chega no ponto, busca um novo ponto aleatório:
        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f){
            _targetPos = GetRandomPointInCircle();
        }
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

    private Vector3 GetRandomPointInCircle(){
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
}
