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

    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerInputManager playerInputManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;

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
    }

    public void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(0, 0, 200, 20), this.GetType().Name + " : " + characterStateMachine.currentState.ToString());
    }
}
