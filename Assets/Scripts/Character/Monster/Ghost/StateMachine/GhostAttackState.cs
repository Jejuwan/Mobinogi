using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GhostAttackState : State
{
    private readonly GhostController monster;
    private AttackPattern selectedPattern;

    public GhostAttackState(GhostController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        selectedPattern = ChooseAvailablePattern();
        if (selectedPattern != null)
        {
            monster.animator.SetTrigger(selectedPattern.animTrigger);
            selectedPattern.Use();
        }
        else
        {
            // 사용 가능한 패턴 없으면 대기 상태로
            monster.stateMachine.SetState(monster.IdleState);
        }
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
    }

    private AttackPattern ChooseAvailablePattern()
    {
        List<AttackPattern> available = monster.attackPatterns
            .Where(p => p.IsAvailable(monster.transform, PlayerController.Instance.transform))
            .ToList();

        if (available.Count == 0)
            return null;

        return available[Random.Range(0, available.Count)];
    }
}
