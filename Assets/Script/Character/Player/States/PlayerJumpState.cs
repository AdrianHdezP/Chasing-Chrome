using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // SFX
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerJump, player.transform.position);

        stateTimer = 1;

        rb.velocity = new Vector2(rb.velocity.x, player.jumpforce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Si detecta la pared y el timer es menor o igual a 0 => wallSlideState
        if (player.IsWallDetected() && stateTimer <= 0)
            stateMachine.ChangeState(player.wallSlideState);

        // Si la velocidad en y es menor que cero => airState
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);

        // Si hay input en x puede moverte en el aire
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 1 * xInput, rb.velocity.y);
    }

}
