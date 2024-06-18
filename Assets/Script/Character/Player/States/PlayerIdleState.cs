using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{

    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }

}
