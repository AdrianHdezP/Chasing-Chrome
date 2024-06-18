using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components

    public GameManager gameManager { get; private set; }
    public EntityFX fx { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr {  get; private set; }

    #endregion

    public Vector2 direction { get; private set; }

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    [HideInInspector] public bool isBusy;

    [Header("Knocknack Config")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    public bool isKnock {  get; private set; }

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
    }

    #region Velocity

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnock)
            return;

        direction = new Vector2(_xVelocity, _yVelocity);

        rb.velocity = direction;
        FlipController(_xVelocity);
    }

    public void SetZeroVelocity()
    {
        if (isKnock)
            return;

        rb.velocity = new Vector2(0, 0);
    }

    #endregion

    #region CharacterFlip

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    #endregion

    #region Damages

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnock = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnock = false;
    }

    public virtual void ReciveDamage()
    {
        Debug.Log(gameObject.name + " Was Damage");

        fx.StartCoroutine(fx.FlashFx());
    }

    #endregion

}
