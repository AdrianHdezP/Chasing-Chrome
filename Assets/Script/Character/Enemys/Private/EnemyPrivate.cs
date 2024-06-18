using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrivate : Enemy
{
    #region States

    public PrivateIdleState idleState {  get; private set; }
    public PrivateMoveState moveState { get; private set; }
    public PrivateBattleState battleState { get; private set; }
    public PrivateAttackState attackState { get; private set; }
    public PrivateDeadState deadState { get; private set; }

    #endregion

    [Header("Collisions Config")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsSlide;
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    public override void Awake()
    {
        base.Awake();

        idleState = new PrivateIdleState(this, stateMachine, "Idle", this);
        moveState = new PrivateMoveState(this, stateMachine, "Move", this);
        battleState = new PrivateBattleState(this, stateMachine, "Move", this);
        attackState = new PrivateAttackState(this, stateMachine, "Attack", this);
        deadState = new PrivateDeadState(this, stateMachine, "Dead", this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);
    }

    public override void Update()
    {
        base.Update();
    }

    #region Damages

    public override void ReciveDamage()
    {
        base.ReciveDamage();

        if (hp <= 0)
            return;

        stateMachine.ChangeState(battleState);
    }

    public override void Die()
    {
        base.Die();

        isDead = true;

        gameManager.AddCredits(creditsAfterDeath);
        stateMachine.ChangeState(deadState);
    }

    #endregion

    #region Collisions

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector3.right * facingDir, wallCheckDistance, whatIsGround);

    //public bool IsSlideDetected() => Physics2D.Raycast(wallCheck.position, Vector3.right * facingDir, wallCheckDistance, whatIsSlide);

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDir, playerCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion

}
