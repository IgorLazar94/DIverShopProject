using System.Collections;
using System.Collections.Generic;

public class PlayerStateMachine
{
    public GenericState CurrentState { get; set; }

    public void Initialize (GenericState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(GenericState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
