using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraffitiState : PlayerState
{
    public PlayerGraffitiState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        gameManager.AddMulti();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
