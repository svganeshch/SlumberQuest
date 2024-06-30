using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Player playerTarget;

    public float detectionRadius;
    public float minimumFOV;
    public float maximumFOV;

    // FSM
    [HideInInspector] public EnemyPursueState enemyPursueState;

    protected override void Awake()
    {
        base.Awake();

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void InitializeStates()
    {
        idleState = new EnemyIdleState(this, characterStateMachine);
        enemyPursueState = new EnemyPursueState(this, characterStateMachine);
    }

    public void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(0, 20, 500, 20), this.GetType().Name + " : " + characterStateMachine.currentState.ToString());
    }
}
