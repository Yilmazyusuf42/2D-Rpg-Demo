using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [HideInInspector]public float stateTimer;
    [Header("Move İnfo")]
    public float idleWaitingTime;
    public float speed;
    [Header("Attack")]
    public float attackDistance;
    public float battleDuration;
    public float battleDistance;
    [SerializeField] float attackCoolDown;
    [HideInInspector]public float lastTimeAttacked;

    public EnemyStateMachine enemyStateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        enemyStateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
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
        => Physics2D.Raycast(wallCheck.transform.position,Vector2.right * facingDir,attackDistance,whatIsPlayer);
}
