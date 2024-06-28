using UnityEngine;

public class JumpState : State
{
    Vector3 characterDirection;
    Vector3 jumpDirection;
    Vector3 freefallDirection;

    public JumpState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        characterDirection = Vector3.zero;

        jumpDirection = GetCharacterDirection();
        SetJumpDirectionVelocity();

        player.playerAnimatorManager.PlayJumpAction();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        freefallDirection = GetCharacterDirection();
        player.controller.Move((player.jumpForwardVelocity * jumpDirection + player.freeFallControlVelocity * freefallDirection) * Time.deltaTime);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetJumpDirectionVelocity()
    {
        if (jumpDirection != Vector3.zero)
        {
            //if (player.characterStateMachine.previousState == player.sprintState)
            //{
            //    jumpDirection *= 1;
            //    Debug.Log("sprint jump");
            //}
            if (player.playerInputManager.moveAmount > 0.5f)
            {
                jumpDirection *= 0.5f;
                Debug.Log("run jump");
            }
            else if (player.playerInputManager.moveAmount <= 0.5f)
            {
                jumpDirection *= 0.25f;
                Debug.Log("normal jump");
            }
        }
    }

    public Vector3 GetCharacterDirection()
    {
        Vector3 characterDirection = player.playerCamera.transform.forward * player.playerInputManager.verticalInput;
        characterDirection += player.playerCamera.transform.right * player.playerInputManager.horizontalInput;
        characterDirection.y = 0;

        return characterDirection.normalized;
    }
}