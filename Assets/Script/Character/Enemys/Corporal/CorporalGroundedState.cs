using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalGroundedState : EnemyState
{
    protected EnemyCorporal enemy;

    public CorporalGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyCorporal _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(enemy.transform.position, gameManager.player.transform.position) < 1)
            stateMachine.ChangeState(enemy.battleState);

        if (enemy.IsGroundDetected() && enemy.IsPlayerDetected() && !enemy.isBusy && !gameManager.isInvisible)
            stateMachine.ChangeState(enemy.battleState);
    }

}
