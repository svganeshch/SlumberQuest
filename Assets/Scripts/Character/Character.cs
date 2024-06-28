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
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public SprintState sprintState;

    [Header("Animation Smoothing")]
    public float speedDampTime = 0.1f;
    public float animationFadeTime = 0.1f;

    [Header("Character controls")]
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationDampTime = 15f;

    [Header("Layer masks")]
    public LayerMask groundLayerMask;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
    }

    protected virtual void Start()
    {
        characterStateMachine = new StateMachine();
        idleState = new IdleState(this, characterStateMachine);
        jumpState = new JumpState(this, characterStateMachine);
        sprintState = new SprintState(this, characterStateMachine);

        characterStateMachine.Initialize(idleState);
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
