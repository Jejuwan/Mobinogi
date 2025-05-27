using System;
using System.Collections.Generic;

public class StateMachine
{
    private State currentState;
    private Dictionary<State, List<StateTransition>> transitions = new();
    private List<StateTransition> anyTransitions = new();

    public void Tick(float deltaTime)
    {
        var transition = GetValidTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        currentState?.Tick(deltaTime);
    }

    public void SetState(State state)
    {
        if (state == currentState) return;

        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void AddTransition(State from, State to, Func<bool> condition)
    {
        if (!transitions.ContainsKey(from))
            transitions[from] = new List<StateTransition>();
        transitions[from].Add(new StateTransition(to, condition));
    }

    public void AddAnyTransition(State to, Func<bool> condition)
    {
        anyTransitions.Add(new StateTransition(to, condition));
    }

    private StateTransition GetValidTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition())
                return transition;
        }

        if (currentState != null && transitions.TryGetValue(currentState, out var stateTransitions))
        {
            foreach (var transition in stateTransitions)
            {
                if (transition.Condition())
                    return transition;
            }
        }

        return null;
    }
}
