using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GravityEnemy : Enemy
{
    public bool gravityMode = false;
    public float gravityModeDuration = 2.5f;
    public float gravityCoolDownDuration = 0.5f;
    public HashSet<GravityEnemy> gravBoisSync = new HashSet<GravityEnemy>();
    int spawnGravBoisCount = 0;

    [HideInInspector] public MeshRenderer meshRenderer;

    [Header("Sfx")]
    public AudioClip gravityEnablingSound;
    public AudioClip gravityCountSound;

    public EnemySpawner spawnSpawner;

    //FSM
    [HideInInspector] public EnemyGravityState enemyGravityState;

    protected override void Awake()
    {
        base.Awake();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        HandleGravBoisCheck();
    }

    private void HandleGravBoisCheck()
    {
        //if (spawnGravBois)
    }

    protected override void InitializeStates()
    {
        base.InitializeStates();

        enemyGravityState = new EnemyGravityState(this, characterStateMachine);

        characterStateMachine.Initialize(enemyGravityState);
    }
}
