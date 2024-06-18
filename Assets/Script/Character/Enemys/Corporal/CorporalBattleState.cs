using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalBattleState : EnemyState
{
    private EnemyCorporal enemy;
    private int moveDir;

    public CorporalBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyCorporal _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetAttackDistance();
        enemy.SetAttackDistanceTimer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.ResetAttackDistance();

        //if (gameManager.isInvisible)
        //    stateMachine.ChangeState(enemy.idleState);

        if (!enemy.IsGroundDetected())
        {
            enemy.SetBusy();
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (gameManager.isInvisible)
                stateMachine.ChangeState(enemy.idleState);

            if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            {
                enemy.SetBusy();
                stateMachine.ChangeState(enemy.idleState);
            }

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance && !enemy.isBusy)
            {
                if (CanAttack() && enemy.IsGroundDetected())
                    stateMachine.ChangeState(enemy.attackState);
                else
                    stateMachine.ChangeState(enemy.idleState);
            }

        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 5)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (player.transform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
