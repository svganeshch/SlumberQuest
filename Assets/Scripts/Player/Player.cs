using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Header("Jump Controls")]
    public float jumpHeight = 0.8f;
    public float jumpForwardVelocity = 5f;
    public float freeFallControlVelocity = 2f;

    [Header("Rewind Controls")]
    public List<Vector3> rewindPoints = new();

    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerInputManager playerInputManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;

    // Input bools
    private bool rewindInput = false;

    public bool isRewinding = false;

    protected override void Awake()
    {
        base.Awake();

        playerCamera = Camera.main;

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
    }

    protected override void Start()
    {
        base.Start();

        playerInputManager.rewindAction.performed += _ => rewindInput = true;
    }

    protected override void Update()
    {
        base.Update();

        HandleInputs();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        rewindPoints.Insert(0, transform.position);
    }

    private void HandleInputs()
    {
        if (rewindInput)
        {
            rewindInput = false;
            SetRewindPoint();
        }
    }

    private void SetRewindPoint()
    {
        transform.position = rewindPoints[0];
        rewindPoints.RemoveAt(0);
    }

    public void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(0, 0, 200, 20), this.GetType().Name + " : " + characterStateMachine.currentState.ToString());
    }
}
