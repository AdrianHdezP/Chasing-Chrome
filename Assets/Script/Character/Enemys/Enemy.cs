using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public BoxCollider2D enemyCollider {  get; private set; }
    
    public EnemyStateMachine stateMachine {  get; private set; }

    [Header("Stats Config")]
    public int hp;
    public int creditsAfterDeath;
    public GameObject creditHUD;
    public GameObject multi2;
    public GameObject multi3;
    public GameObject multi4;
    public GameObject multi5;

    [Header("Move Config")]
    public float idleTime;
    public float moveSpeed;
    public float battleTime;

    [Header("Attack Config")]
    public float attackDistance;
    private float defaultAttackDistance;
    private float attackDistanceTimer;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("AI Config")]
    [SerializeField] private float lostPlayerTime;
    private float busyTimer;

    [HideInInspector] public bool isDead;

    public override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    public override void Start()
    {
        base.Start();

        enemyCollider = GetComponent<BoxCollider2D>();

        #region Setup Variables

        isBusy = false;
        defaultAttackDistance = attackDistance;
        SetAttackDistanceTimer();

        #endregion

    }

    public override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        DefaultBusy();
    }

    #region Busy

    public void SetBusy()
    {
        isBusy = true;
        busyTimer = lostPlayerTime;
    }

    private void DefaultBusy()
    {
        if (isBusy)
        {
            busyTimer -= Time.deltaTime;

            if (busyTimer < 0)
                isBusy = false;
        }

        if (Vector2.Distance(transform.position, gameManager.player.transform.position) < 0.5f)
            isBusy = false;
    }

    #endregion

    #region Damages

    public virtual void Kncockback()
    {
        StartCoroutine(HitKnockback());
    }

    #region Set & Reset Attack Distance

    public void SetAttackDistanceTimer() => attackDistanceTimer = attackCooldown;

    public void SetAttackDistance() => attackDistance = 0.1f;

    public void ResetAttackDistance()
    {
        if (attackDistance != defaultAttackDistance)
        {
            attackDistanceTimer -= Time.deltaTime;

            if (attackDistanceTimer <= 0)
                attackDistance = defaultAttackDistance;
        }
    }

    #endregion

    public virtual void EnemyDamage(int _amountOfDamage)
    {
        hp -= _amountOfDamage;
        ReciveDamage();
    }

    public override void ReciveDamage()
    {
        base.ReciveDamage();

        AudioManager.instance.PlayOneShoot(FMODEvents.instance.enemyHit, transform.position);

        if (hp <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " Is Dead");
        StartCoroutine(Credits());
        rb.bodyType = RigidbodyType2D.Static;
        enemyCollider.enabled = false;
    }

    private IEnumerator Credits()
    {
        creditHUD.gameObject.SetActive(true);

        if (gameManager.pointsMultiValue == 2)
            multi2.gameObject.SetActive(true);

        if (gameManager.pointsMultiValue == 3)
            multi3.gameObject.SetActive(true);

        if (gameManager.pointsMultiValue == 4)
            multi4.gameObject.SetActive(true);

        if (gameManager.pointsMultiValue == 5)
            multi5.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        creditHUD.gameObject.SetActive(false);
        multi2.gameObject.SetActive(false);
        multi3.gameObject.SetActive(false);
        multi5.gameObject.SetActive(false);
    }

    #endregion

    #region AnimationEvents

    public virtual void AnimationFinishTriggers() => stateMachine.currentState.AnimationFinishTriggers();

    #endregion

}
