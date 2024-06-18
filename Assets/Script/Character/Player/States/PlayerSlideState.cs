using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerSlideState : PlayerState
{

    public PlayerSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim) : base(_player, _stateMachine, _animBoolAnim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // SFX
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerSlide, player.transform.position);

        // Timer equal to slideDuration
        stateTimer = player.slideDuration;

        // Normal collider off and slide collider on
        normalCollider.enabled = false;
        slideCollider.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();

        // La velocidad en x es 0 y la y conserva ssu velocidad
        player.SetVelocity(0, rb.velocity.y);

        // Si se detecta el techo del slide entonces la velocidad es 0 
        if (player.IsSlideDetected())
            player.SetZeroVelocity();

        // Normal collider on and slide collider off
        normalCollider.enabled = true;
        slideCollider.enabled = false;

        player.isDoingGraffiti = false;
    }

    public override void Update()
    {
        base.Update();

        // Si no detecta el suelo y detecta la pared => wallSlideState
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        // Si no detecta el suelo y el state timer es menor que la mitad de la duracion => airState
        if (!player.IsGroundDetected() && stateTimer < player.slideDuration / 2)
            stateMachine.ChangeState(player.airState);

        // Impulso 
        player.SetVelocity(player.slideSpeed * player.facingDir, 0);

        // Si el timer es menor que 0 => moveState
        if (stateTimer < 0)
            stateMachine.ChangeState(player.moveState);
    }

}
