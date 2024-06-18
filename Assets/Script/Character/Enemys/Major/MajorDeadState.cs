using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MajorDeadState : EnemyState
{
    private EnemyMajor enemy;

    public MajorDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMajor _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.majorDeath, enemy.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
