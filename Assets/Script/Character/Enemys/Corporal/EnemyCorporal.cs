using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorporal : Enemy
{

    #region States

    public CorporalIdleState idleState {  get; private set; }
    public CorporalMoveState moveState { get; private set; }
    public CorporalBattleState battleState { get; private set; }
    public CorporalAttackState attackState { get; private set; }
    public CorporalDeadState deadState { get; private set; }

    #endregion

    [Header("Bullets Config")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed;

    [Header("Collisions Config")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    public override void Awake()
    {
        base.Awake();

        idleState = new CorporalIdleState(this, stateMachine, "Idle", this);
        moveState = new CorporalMoveState(this, stateMachine, "Move", this);
        battleState = new CorporalBattleState(this, stateMachine, "Move", this);
        attackState = new CorporalAttackState(this, stateMachine, "Attack", this);
        deadState = new CorporalDeadState(this, stateMachine, "Dead", this);
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

    #region Attack & Damages

    public void Shoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        _bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;

        Destroy(_bullet, 2.5f);
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
    }

    #endregion

}
