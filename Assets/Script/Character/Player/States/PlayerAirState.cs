using UnityEngine;

public class PlayerAirState : PlayerState
{

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
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

        // Si toca la pared => wallSlideState
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        // Si detecta el suelo pero no la parde => idleState
        if (player.IsGroundDetected() && !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        // Si detecta el suelo => idleState
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        // Si no hay input en x no hay velocidad en x
        if (xInput == 0)
            rb.velocity = new Vector2(0, rb.velocity.y);

        // Si hay input en x puedes moverte en el aire
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 1 * xInput, rb.velocity.y);
    }

}
