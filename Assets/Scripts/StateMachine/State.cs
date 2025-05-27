using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Tick(float deltaTime) { }
    public virtual void Exit() { }
}
