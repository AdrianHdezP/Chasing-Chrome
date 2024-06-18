using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyMajor : Enemy
{
    #region States

    public MajorIdleState idleState { get; private set; }
    public MajorMoveState moveState { get; private set; }
    public MajorBattleState battleState { get; private set; }
    public MajorAttackState attackState { get; private set; }
    public MajorBusyState busyState { get; private set; }
    public MajorDeadState deadState { get; private set; }

    #endregion

    private bool isStuned;

    [Header("Collisions Config")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    public Transform attackCheck;
    public float attackCheckRadius;

    public override void Awake()
    {
        base.Awake();

        idleState = new MajorIdleState(this, stateMachine, "Idle", this);
        moveState = new MajorMoveState(this, stateMachine, "Move", this);
        battleState = new MajorBattleState(this, stateMachine, "Move", this);
        attackState = new MajorAttackState(this, stateMachine, "Attack", this);
        busyState = new MajorBusyState(this, stateMachine, "Busy", this);
        deadState = new MajorDeadState(this, stateMachine, "Dead", this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);

        isStuned = false;
    }

    public override void Update()
    {
        base.Update();
    }

    public void SetStuned() => isStuned = true;

    public void ResetStuned() => isStuned = false;

    #region Damages

    public override void EnemyDamage(int _amountOfDamage)
    {
        if (!isStuned)
        {
            stateMachine.ChangeState(busyState);
            return;
        }

        base.EnemyDamage(_amountOfDamage);
    }

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

        gameManager.AddCredits(creditsAfterDeath);
        stateMachine.ChangeState(deadState);
    }

    #endregion

    #region Collisions

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector3.right * facingDir, wallCheckDistance, whatIsGround);

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
