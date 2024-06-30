using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueState : State
{
    public EnemyPursueState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.navMeshAgent.SetDestination(enemy.playerTarget.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.navMeshAgent.SetDestination(enemy.playerTarget.transform.position);

        if (enemy.TryGetComponent<GravityEnemy>(out GravityEnemy gravityEnemy))
        {
            stateMachine.ChangeState(gravityEnemy.enemyGravityState);
        } 
    }
}
