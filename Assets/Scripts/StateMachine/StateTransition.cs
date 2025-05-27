using System;

public class StateTransition
{
    public State To { get; }
    public Func<bool> Condition { get; }

    public StateTransition(State to, Func<bool> condition)
    {
        To = to;
        Condition = condition;
    }
}
