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

    public EnemyGravityState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        enemy = base.enemy as GravityEnemy;
        gravityEnablingClip = enemy.gravityEnablingSound;
        enemyMat = enemy.meshRenderer.material;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.spawnGravBois.Add(enemy);

        enemyMatDefColor = enemyMat.color;
        enemyMat.color = Color.red;
        //PlayGravityEnablingSound();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.navMeshAgent.SetDestination(enemy.playerTarget.transform.position);

        if (enemy.gravityMode)
        {
            UpdateGravityMode();
        }
        else
        {
            UpdateCoolDownMode();
        }
    }

    private void UpdateGravityMode()
    {
        gravityTime += Time.deltaTime;

        //AdjustPitchDuringGravity();

        if (gravityTime > enemy.gravityModeDuration)
        {
            enemy.gravityMode = false;
            gravityTime = 0;

            //ReversePlayback();
            enemyMat.color = enemyMatDefColor;
        }
    }

    private void UpdateCoolDownMode()
    {
        coolDownTime += Time.deltaTime;

        //AdjustPitchDuringCoolDown();

        if (coolDownTime >= enemy.gravityCoolDownDuration)
        {
            coolDownTime = 0;
            enemy.gravityMode = true;

            //ResetPlayback();
            enemyMat.color = Color.red;
        }
    }

    private void AdjustPitchDuringGravity()
    {
        enemy.audioSource.pitch -= gravityTime * audioStartPitch / 5;
    }

    private void AdjustPitchDuringCoolDown()
    {
        enemy.audioSource.pitch += coolDownTime * audioStartPitch / 5;
    }

    private void PlayGravityEnablingSound()
    {
        audioStartPitch = 0.5f;
        enemy.audioSource.clip = gravityEnablingClip;
        enemy.audioSource.Play();
    }

    private void ReversePlayback()
    {
        if (!isReversed)
        {
            isReversed = true;
            enemy.audioSource.Stop();
            enemy.audioSource.clip = gravityEnablingClip;
            enemy.audioSource.pitch = -audioStartPitch;
            enemy.audioSource.Play();
        }
    }

    private void ResetPlayback()
    {
        isReversed = false;
        enemy.audioSource.Stop();
        enemy.audioSource.clip = gravityEnablingClip;
        enemy.audioSource.pitch = audioStartPitch;
        enemy.audioSource.Play();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.audioSource.pitch = 1.0f;
    }
}