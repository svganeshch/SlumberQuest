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
    [HideInInspector] public InputAction moveAction;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        moveAction = input.actions["Move"];
        moveAction.performed += value => moveInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;
    }
}
