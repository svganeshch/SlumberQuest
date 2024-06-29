using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterController controller;

    [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;

    // Animation flags
    public bool canMove = true;
    public bool canRotate = true;
    public bool applyRootMotion = false;

    //FSM
    [HideInInspector] public StateMachine characterStateMachine;
    [HideInInspector] public State idleState;
    [HideInInspector] public State jumpState;
    [HideInInspector] public State sprintState;

    [Header("Animation Smoothing")]
    public float speedDampTime = 0.1f;
    public float animationFadeTime = 0.1f;

    [Header("Character controls")]
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationDampTime = 15f;
    public Transform lineCast;

    protected virtual void InitializeStates() { }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();

        characterStateMachine = new StateMachine();
    }

    protected virtual void Start()
    {
        InitializeStates();
    }

    protected virtual void Update()
    {
        characterStateMachine.currentState.HandleInput();
        characterStateMachine.currentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        characterStateMachine.currentState.PhysicsUpdate();
    }
}
