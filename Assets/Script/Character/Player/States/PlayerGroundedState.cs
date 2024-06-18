using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
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

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        controls.Player.Jump.performed += context =>
        {
            if (!player.canMove)
                return;

            if (player.IsGroundDetected() && !player.isDoingGraffiti && !player.isUsingPowerup)
                stateMachine.ChangeState(player.jumpState);
        };

        controls.Player.Attack.performed += context =>
        {
            if (!player.canMove)
                return;

            if (!player.isBusy && player.isAttacking == false && !player.isDoingGraffiti)
                stateMachine.ChangeState(player.attackState);
        };

        controls.Player.Shoot.performed += context =>
        {
            if (!player.canMove)
                return;

            if (gameManager.currentBullets >= 1 && player.IsGroundDetected() && !player.isDoingGraffiti)
                stateMachine.ChangeState(player.shootState);
        };
    }

}
