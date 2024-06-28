using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private bool jump = false;
    private bool sprint = false;

    public IdleState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jump = false;
        sprint = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.playerInputManager.jumpAction.WasPressedThisFrame())
        {
            jump = true;
        }

        if (player.playerInputManager.sprintAction.WasPressedThisFrame())
        {
            sprint = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (jump)
        {
            Debug.Log("jump pressed");
            jump = false;
            stateMachine.ChangeState(player.jumpState);
        }

        if (sprint)
        {
            sprint = false;
            stateMachine.ChangeState(player.sprintState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
