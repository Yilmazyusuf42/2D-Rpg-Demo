using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;


    private float freezeDuration;
    private float defaultSpeed;


    protected bool isStunned;
    [SerializeField] GameObject stunnedIcon;
    [HideInInspector] public float stateTimer;


    [Header("Move İnfo")]
    public float idleWaitingTime;
    public float speed;


    [Header("Attack")]
    public float attackDistance;
    public float battleDuration;
    public float battleDistance;
    [SerializeField] float attackCoolDown;
    [HideInInspector] public float lastTimeAttacked;



    public EnemyStateMachine enemyStateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ClosePerryIcon();
        enemyStateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        enemyStateMachine.currentState.Update();
        stateTimer -= Time.deltaTime;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.transform.position,
            new Vector3(wallCheck.transform.position.x + attackDistance,
            wallCheck.transform.position.y,
            wallCheck.transform.position.z));

    }

    public virtual void AnimationFinishTrigger() => enemyStateMachine.currentState.AnimationFinished();

    public virtual bool CanAttack() => (lastTimeAttacked + attackCoolDown <= Time.time) ? true : false;
    public virtual RaycastHit2D IsPlayerDetected()
        => Physics2D.Raycast(wallCheck.transform.position, Vector2.right * facingDir, attackDistance, whatIsPlayer);

    public override IEnumerator TakingDamage()
    {
        isDamaged = true;
        rb.velocity = new Vector2(damageShaking.x * ((IsPlayerDetected()) ? -facingDir : facingDir), damageShaking.y);
        yield return new WaitForSeconds(takingDamageDuration);
        isDamaged = false;
    }

    public virtual void ShowPerryIcon()
    {
        isStunned = true;
        stunnedIcon.SetActive(true);

    }
    public virtual void ClosePerryIcon()
    {
        isStunned = false;
        stunnedIcon.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (isStunned)
            return true;

        return false;

    }

    public void BeingFreeze(bool _status)
    {
        if (_status)
        {
            anim.speed = 0;
            speed = 0;
        }
        else
        {
            anim.speed = 1;
            speed = defaultSpeed;
        }
    }

    public IEnumerator FreezingFor(float _freezeDuration)
    {
        BeingFreeze(true);
        yield return new WaitForSeconds(_freezeDuration);
        BeingFreeze(false);
    }
}
