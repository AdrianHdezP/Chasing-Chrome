using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();

        player.AttackForce(50, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        player.isAttacking = false;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
