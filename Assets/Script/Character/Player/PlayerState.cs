using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD.Studio;

public class PlayerState
{
    protected GameManager gameManager;

    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;
    protected Controls controls;

    protected CapsuleCollider2D normalCollider;
    protected CircleCollider2D slideCollider;

    private string animBoolName;
    protected bool triggerCalled;

    protected float stateTimer;

    protected float xInput;
    protected float yInput;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolAnim)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolAnim;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);

        gameManager = player.gameManager;

        controls = player.controls;
        rb = player.rb;

        normalCollider = player.capsuleCollider;
        slideCollider = player.slideCollider;

        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");

        xInput = player.xInput;
        yInput = player.yInput;
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
