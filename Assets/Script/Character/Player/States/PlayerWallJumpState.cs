public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerJump, player.transform.position);

        stateTimer = .4f;

        player.SetVelocity(5 * -player.facingDir, player.jumpforce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        // Dejar que se mueva en el aire
        if (xInput != 0 && stateTimer <= .25f)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
