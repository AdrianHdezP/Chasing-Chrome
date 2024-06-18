using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseGranade : PlayerState
{
    public PlayerUseGranade(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.isUsingPowerup = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
