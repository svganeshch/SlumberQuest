using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterMovementManager : MonoBehaviour
{
    protected Character character;
    private Camera playerCamera;

    // input
    protected float verticalInput;
    protected float horizontalInput;
    private Vector3 moveDirection;

    // ground checks
    [HideInInspector] public Vector3 yVelocity;
    [HideInInspector] public float gravityForce = -40;
    protected float groundCheckSphereRadius = 0.3f;
    protected float groundedYVelocity = -20;
    protected float fallStartYVelocity = -5;
    protected bool fallingVelocitySet = false;
    private bool isGrounded = false;

    // rotation
    Vector3 targetRotationDirection;
    Quaternion targetRotation;
    Quaternion finalRotation;

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        playerCamera = Camera.main;
    }

    protected virtual void Start() { }

    public virtual void Update()
    {
        HandleGroundedMovement();
        HandleGroundCheck();
        HandleRotation();
    }

    protected virtual void GetMovementInput() { }

    protected virtual void HandleGroundedMovement()
    {
        GetMovementInput();

        moveDirection = playerCamera.transform.forward * verticalInput;
        moveDirection += playerCamera.transform.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        float speed = character.walkingSpeed;

        character.controller.Move(speed * Time.deltaTime * moveDirection);
    }

    protected virtual void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, character.groundLayerMask);

        if (isGrounded)
        {
            if (yVelocity.y < 0f)
            {
                fallingVelocitySet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            //if (character.characterStateMachine.currentState != character.jumpState && !fallingVelocitySet)
            //{
            //    fallingVelocitySet = true;
            //    yVelocity.y = fallStartYVelocity;
            //}

            yVelocity.y += gravityForce * Time.deltaTime;
        }

        character.controller.Move(yVelocity * Time.deltaTime);
    }

    protected virtual void HandleRotation()
    {
        targetRotationDirection = playerCamera.transform.forward * verticalInput;
        targetRotationDirection += playerCamera.transform.right * horizontalInput;
        targetRotationDirection.y = 0f;
        targetRotationDirection.Normalize();

        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = character.transform.forward;
        }

        targetRotation = Quaternion.LookRotation(targetRotationDirection);

        finalRotation = Quaternion.Slerp(character.transform.rotation, targetRotation, character.rotationDampTime * Time.deltaTime);
        character.transform.rotation = finalRotation;
    }
}
