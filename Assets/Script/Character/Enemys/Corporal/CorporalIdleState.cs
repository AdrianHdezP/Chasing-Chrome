using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalIdleState : CorporalGroundedState
{
    public CorporalIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyCorporal _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();

        if (enemy.isKnock)
            return;

        enemy.Flip();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
