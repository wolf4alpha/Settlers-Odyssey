public class VillagerStateMachine
{
    public VillagerState currentState { get; private set; }

    public void Initialize(VillagerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(VillagerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();

    }

}
