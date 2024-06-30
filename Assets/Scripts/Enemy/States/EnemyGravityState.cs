using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGravityState : State
{
    private new GravityEnemy enemy;

    private float coolDownTime = 0;
    private float gravityTime = 0;

    private AudioClip gravityEnablingClip;
    private bool isReversed = false;

    private float audioStartPitch;
    private float audioTime;

    private Material enemyMat;
    private Color enemyMatDefColor;

    int gcount = 0;

    public EnemyGravityState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        enemy = base.enemy as GravityEnemy;
        gravityEnablingClip = enemy.gravityEnablingSound;
        enemyMat = enemy.meshRenderer.material;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.gravBoisSync.Add(enemy);
        enemy.gravityMode = true;

        enemyMatDefColor = enemyMat.color;
        enemyMat.color = Color.red;
        enemy.audioSource.PlayOneShot(enemy.gravityCountSound);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //enemy.navMeshAgent.SetDestination(enemy.playerTarget.transform.position);

        if (enemy.gravityMode)
        {
            UpdateGravityMode();
        }
        else
        {
            UpdateCoolDownMode();
        }

        if (gcount >= 3)
        {
            enemy.spawnSpawner.gameObject.GetComponent<FlipObject>().flipSet = true;
            enemy.spawnSpawner.target.transform.SetParent(enemy.spawnSpawner.gameObject.transform);
            gcount = 0;
        }
    }

    private void UpdateGravityMode()
    {
        gravityTime += Time.deltaTime;

        if (gravityTime > enemy.gravityModeDuration)
        {
            enemy.gravityMode = false;
            gravityTime = 0;

            enemyMat.color = enemyMatDefColor;
        }
    }

    private void UpdateCoolDownMode()
    {
        coolDownTime += Time.deltaTime;

        if (coolDownTime >= enemy.gravityCoolDownDuration)
        {
            coolDownTime = 0;
            enemy.gravityMode = true;

            enemyMat.color = Color.red;
            enemy.audioSource.PlayOneShot(enemy.gravityCountSound);
            gcount++;
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.audioSource.pitch = 1.0f;
    }
}