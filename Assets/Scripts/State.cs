using UnityEngine;

public class State
{
    protected Character character;
    protected Player player;
    protected Enemy enemy;
    protected StateMachine stateMachine;

    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        if (_character is Player p)
        {
            player = p;
        }
        else if (_character is Enemy e)
        {
            enemy = e;
        }
    }

    public virtual void Enter()
    {
        Debug.Log("Entered state " + character.name + " : " + this);
    }

    public virtual void HandleInput() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit()
    {
        Debug.Log("Exited state " + character.name + " : " + this);
    }
}