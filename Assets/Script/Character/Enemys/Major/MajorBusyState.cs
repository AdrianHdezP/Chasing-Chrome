using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MajorBusyState : EnemyState
{
    private EnemyMajor enemy;
    public MajorBusyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMajor _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.majorBusy, enemy.transform.position);

        enemy.SetStuned();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.ResetStuned();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
