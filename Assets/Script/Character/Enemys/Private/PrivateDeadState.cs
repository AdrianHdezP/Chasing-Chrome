using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateDeadState : EnemyState
{
    private EnemyPrivate enemy;

    public PrivateDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyPrivate _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.privateDeath, enemy.transform.position);
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
