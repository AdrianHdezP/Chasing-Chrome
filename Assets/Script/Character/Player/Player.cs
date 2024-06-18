using System.Collections;
using UnityEngine;
using FMOD.Studio;

public class Player : Entity
{

    #region Components

    public Controls controls { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public CircleCollider2D slideCollider { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerSlideState slideState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerShootState shootState { get; private set; }
    public PlayerAttackState attackState { get; private set; }

    public PlayerGraffitiState graffitiState { get; private set; }
    public PlayerTackleState tackleState { get; private set; }
    public PlayerUseGranade useGranade { get; private set; }
    #endregion

    #region Variables

    [Header("Graffiti Text")]
    public GameObject graffitiTextPC;
    public GameObject graffitiTextController;

    #region Sound

    private EventInstance playerFootsteps;

    #endregion

    #region Inputs

    [HideInInspector] public bool canMove;

    public float xInput {  get; private set; }
    public float yInput { get; private set; }

    #endregion

    [Header("Stats Config")]
    public int damage;

    [Header("Move Config")]
    public float moveSpeed = 5f;
    public float jumpforce = 10f;

    [Header("Slide Config")]
    [SerializeField] private float slideCooldown;
    public float slideSpeed;
    public float slideDuration;

    public float slideUsageTimer { get; private set; }

    [Header("Attack Config")]
    public Vector2[] attackMovement;
    [HideInInspector] public bool isAttacking;

    [Header("Bullets Config")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed;

    [Header("Granade Config")]
    [SerializeField] private GameObject stunGranadePrefab;
    [SerializeField] private Transform stunGranadeSpawnPoint;
    [SerializeField] private float stunGranadeSpeed;
    [SerializeField] private Vector2 stunGranadeDir;

    [Header("Checkpoints")]
    [SerializeField] private GameObject[] checkpoints;

    private Vector2 respawnPoint;

    [Header("Collisions Config")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform slideCheck;
    [SerializeField] private float slideCheckDistance;
    [SerializeField] private LayerMask whatIsSlide;
    public Transform attackCheck;
    public float attackCheckRadius;

    [HideInInspector] public bool isDoingGraffiti = false;
    [HideInInspector] public bool isUsingPowerup = false;

    #endregion

    public override void Awake()
    {
        base.Awake();

        controls = new Controls();

        #region States Machine && States

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        slideState = new PlayerSlideState(this, stateMachine, "Slide");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        shootState = new PlayerShootState(this, stateMachine, "Shoot");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");

        graffitiState = new PlayerGraffitiState(this, stateMachine, "Graffiti");
        tackleState = new PlayerTackleState(this, stateMachine, "Tackle");
        useGranade = new PlayerUseGranade(this, stateMachine, "Granade");

        #endregion

        #region Setups Booleans

        canMove = true;
        isAttacking = false;
        isDoingGraffiti = false;
        isUsingPowerup = false;

        #endregion
    }

    #region Enable and Disable Controls

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion

    public override void Start()
    {
        base.Start();
        
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        slideCollider = GetComponentInChildren<CircleCollider2D>();

        stateMachine.Initialize(idleState);

        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);

        slideCollider.enabled = false;

        isAttacking = false;
        isDoingGraffiti = false;
        isUsingPowerup = false;

        respawnPoint = transform.position;
    }

    public override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        Inputs();
        Powerup();
        CheckForSlide();
        SlideDamage();

        IsDoingGraffiti();
        FootstepsSound();
    }

    #region Controls

    private void Inputs()
    {
        if (canMove)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            // Deathzone Joystick
            if (xInput > .01)
                xInput = 1;
            else if (xInput < -.01)
                xInput = -1;
            else
                xInput = 0;
        }
        else
        {
            xInput = 0;
            yInput = 0;
        }
    }

    private void Powerup()
    {
        controls.Player.UseItem.performed += context =>
        {
            if (!canMove)
                return;

            gameManager.UsePowerups();
        };
    }

    #endregion

    #region Slide

    public void CheckForSlide()
    {
        slideUsageTimer -= Time.deltaTime;

        controls.Player.Slide.performed += context =>
        {
            if (!canMove)
                return;

            if (IsWallDetected())
                return;

            if (!IsGroundDetected())
                return;

            if (slideUsageTimer < 0 && Input.GetAxisRaw("Horizontal") != 0 && !isDoingGraffiti && !isUsingPowerup)
                SlideAction();
        };
    }

    private void SlideAction()
    {
        slideUsageTimer = slideCooldown;

        stateMachine.ChangeState(slideState);
    }

    #endregion

    #region AnimationEvents

    public void AnimationTriggers() => stateMachine.currentState.AnimationFinishTrigger();

    public void AnimationShoot()
    {
        Shoot();

        gameManager.UpdateBullets();
    }

    public void Shoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        _bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;

        Destroy(_bullet, 2.5f);
    }

    public void UseStunGrande()
    {
        GameObject _stunGranade = Instantiate(stunGranadePrefab, stunGranadeSpawnPoint.position, stunGranadeSpawnPoint.rotation);
        _stunGranade.GetComponent<Rigidbody2D>().velocity = stunGranadeDir;
    }

    #endregion

    #region TriggersDetections

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item _itemDetected = collision.GetComponent<Item>();

        if (_itemDetected != null)
        {
            _itemDetected.UseItem();
            _itemDetected.SavePowerup();
        }

        DeathZone_Tag _deathZone = collision.GetComponent<DeathZone_Tag>();

        if (_deathZone != null)
        {
            ReciveDamage();
            transform.position = respawnPoint;
            AudioManager.instance.PlayOneShoot(FMODEvents.instance.respawnCheckpointSFX, transform.position);
        }

        Checkpoint_Tag _checkpoint = collision.GetComponent<Checkpoint_Tag>();

        if (_checkpoint != null)
        {
            AudioManager.instance.PlayOneShoot(FMODEvents.instance.checkpointSFX, transform.position);
            respawnPoint = transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Graffiti graffiti = collision.GetComponent<Graffiti>();

        if (graffiti != null)
        {
            if (ControlDevice.instance.IsGamepadActive())
                graffitiTextController.SetActive(true);
            else
                graffitiTextPC.SetActive(true);

            controls.Player.UseItem.performed += context =>
            {
                if (!canMove)
                    return;

                graffiti.UseItem();
            };  
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Graffiti graffiti = collision.GetComponent<Graffiti>();

        if (graffiti != null)
        {
            graffitiTextController.SetActive(false);
            graffitiTextPC.SetActive(false);
        }
    }

    #endregion

    #region Attack & Damages

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().isDead == true)
                    return;

                transform.position = hit.transform.position;
                AttackForce(25, rb.velocity.y);

                hit.GetComponent<Enemy>().EnemyDamage(damage);
            }
        }
    }

    public void AttackForce(float x, float y) => rb.AddForce(new Vector2(x * facingDir, y), ForceMode2D.Impulse);

    public override void ReciveDamage()
    {
        base.ReciveDamage();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerHit, transform.position);

        gameManager.RestartMulti();

        if (gameManager.creditsValue <= 0)
            return;

        gameManager.SubtractCredits(25);
    }

    private void SlideDamage()
    {
        if (IsSlideDetected() && rb.velocity.x == 0)
        {
            ReciveDamage();
            transform.position = respawnPoint;
        }
    }

    #endregion

    #region Collisions

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector3.right * -facingDir, wallCheckDistance, whatIsGround);

    public bool IsSlideDetected() => Physics2D.Raycast(slideCheck.position, Vector2.up, slideCheckDistance, whatIsSlide);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawLine(slideCheck.position, new Vector3(slideCheck.position.x, slideCheck.position.y + slideCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion

    private void IsDoingGraffiti()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerGraffiti"))
            isDoingGraffiti = true;
        else
            isDoingGraffiti = false;
    }

    private void FootstepsSound()
    {
        if (xInput != 0 && IsGroundDetected() && !IsWallDetected())
        {

            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                playerFootsteps.start();

            if (IsWallDetected() || isDoingGraffiti)
                playerFootsteps.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            if (gameManager.isPaused)
                playerFootsteps.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else
            playerFootsteps.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}