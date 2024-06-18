using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalDeadState : EnemyState
{
    private EnemyCorporal enemy;

    public CorporalDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyCorporal _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.corporalDeath, enemy.transform.position);
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
