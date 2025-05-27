public class MonsterIdleState : State
{
    private readonly MonsterController monster;

    public MonsterIdleState(MonsterController mst, StateMachine sm) : base(sm)
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
