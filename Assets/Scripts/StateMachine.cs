public class StateMachine
{
    public State currentState;
    public State previousState;

    public void Initialize(State initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}