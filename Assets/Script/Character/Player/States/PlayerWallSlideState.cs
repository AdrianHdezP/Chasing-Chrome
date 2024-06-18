using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
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

        yInput = 0;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .9f);

        if (xInput == 0)
        {
            player.SetVelocity(5 * -player.facingDir, yInput);
            stateMachine.ChangeState(player.idleState);
        }

        if (player.facingDir == -1)
            if (xInput == 1)
                stateMachine.ChangeState(player.idleState);

        if (player.facingDir == 1)
            if (xInput == -1)
                stateMachine.ChangeState(player.idleState);


        if (!player.IsWallDetected())
            stateMachine.ChangeState(player.airState);

        controls.Player.WallJump.performed += context =>
        {
            if (stateMachine.currentState == player.wallSlideState)
                stateMachine.ChangeState(player.wallJumpState);
        };

        // Hacer que baje con la S
        //if (yInput < 0)
        //    rb.velocity = new Vector2(0, rb.velocity.y);
        //else
        //    rb.velocity = new Vector2(0.32f * player.facingDir, rb.velocity.y * .7f);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
