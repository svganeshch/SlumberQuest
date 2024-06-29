using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GravityEnemy : Enemy
{
    public bool gravityMode = false;
    public float gravityModeDuration = 2.5f;
    public float gravityCoolDownDuration = 0.5f;
    public HashSet<GravityEnemy> spawnGravBois = new HashSet<GravityEnemy>();

    [HideInInspector] public MeshRenderer meshRenderer;

    [Header("Sfx")]
    public AudioClip gravityEnablingSound;

    //FSM
    [HideInInspector] public EnemyGravityState enemyGravityState;

    protected override void Awake()
    {
        base.Awake();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void InitializeStates()
    {
        base.InitializeStates();

        enemyGravityState = new EnemyGravityState(this, characterStateMachine);
    }
}
