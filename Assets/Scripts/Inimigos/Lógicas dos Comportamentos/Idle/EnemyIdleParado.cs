using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Parado", menuName = "Lógica do Inimigo/Idle/Parado")]
public class EnemyIdleParado : EnemyIdleSOBase
{
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
