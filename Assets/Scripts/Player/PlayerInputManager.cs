using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public float moveAmount;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [HideInInspector] public PlayerInput input;

    // Input actions
    [HideInInspector] public InputAction collectAction;
    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction flipAction;
    [HideInInspector] public InputAction jumpAction;
    [HideInInspector] public InputAction rewindAction;
    [HideInInspector] public InputAction sprintAction;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        collectAction = input.actions["Collect"];
        flipAction = input.actions["Flip"];
        jumpAction = input.actions["Jump"];
        rewindAction = input.actions["Rewind"];
        sprintAction = input.actions["Sprint"];

        moveAction = input.actions["Move"];
        moveAction.performed += value => moveInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if (moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5f && moveAmount <= 1)
        {
            moveAmount = 1;
        }
    }
}
