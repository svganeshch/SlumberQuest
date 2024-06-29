using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    public EnemyIdleState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DetectTarget();
    }

    private void DetectTarget()
    {
        //if (enemy.isDead) return;
        //Debug.Log("looking for target");

        Collider[] colliders = new Collider[1];
        int colliderCount = Physics.OverlapSphereNonAlloc(
            enemy.transform.position,
            enemy.detectionRadius,
            colliders,
            LayerMaskManager.instance.playerLayerMask
        );

        for (int i = 0; i < colliderCount; i++)
        {
            //Debug.Log("collider : " + colliders[i].gameObject.name);
            if (!colliders[i].transform.TryGetComponent(out Player targetCharacter)) continue;

            //Debug.Log("target : " + targetCharacter.name);
            Vector3 targetDirection = targetCharacter.transform.position - enemy.transform.position;
            float angleToTarget = Vector3.Angle(targetDirection, enemy.transform.forward);

            if (angleToTarget > enemy.minimumFOV && angleToTarget < enemy.maximumFOV)
            {
                //Debug.Log("checking if in fov");
                if (!Physics.Linecast(enemy.lineCast.position, targetCharacter.lineCast.position, LayerMaskManager.instance.obstacleLayerMask))
                {
                    enemy.playerTarget = targetCharacter;
                    //Debug.Log("Target found");

                    stateMachine.ChangeState(enemy.enemyPursueState);
                    return;
                }
            }
        }
    }
}
