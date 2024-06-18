using UnityEngine;

public class EnemyState
{
    protected GameManager gameManager;
    protected Player player;

    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    protected Rigidbody2D rb;

    private string animBoolName;
    protected bool triggerCalled;

    protected float stateTimer;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true);

        gameManager = enemyBase.gameManager;
        player = gameManager.player;

        rb = enemyBase.rb;

        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTriggers()
    {
        triggerCalled = true;
    }
}
