public class SprintState : State
{
    bool sprint;
    bool sprintJump;

    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sprint = true;
        sprintJump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.playerInputManager.sprintAction.WasPressedThisFrame() || player.playerInputManager.moveInput.sqrMagnitude == 0f)
        {
            sprint = false;
        }

        if (player.playerInputManager.jumpAction.WasPressedThisFrame())
        {
            sprintJump = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!sprint)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (sprintJump)
        {
            stateMachine.ChangeState(player.jumpState);
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