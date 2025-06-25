public class GoblinIdleState : State
{
    private readonly MonsterController monster;

    public GoblinIdleState(MonsterController mst, StateMachine sm) : base(sm)
    {
        this.monster = mst;
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
    }
}
