using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleState : PlayerState
{
    public PlayerTackleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1.25f;
    }

    public override void Exit()
    {
        base.Exit();

        player.isUsingPowerup = false;

        gameManager.isDoingTackle = false;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer <= 1)
            rb.velocity = new Vector2(gameManager.tackleForce * player.facingDir, rb.velocity.y);

        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
    }
}
