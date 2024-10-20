using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemySkeleton : Enemy
{

    #region states
    public SkaletonIdleState idleState { get; private set; }
    public SkaletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttack skeletonAttack { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SkaletonIdleState(this,enemyStateMachine,"Idle",this);
        moveState = new SkaletonMoveState(this, enemyStateMachine, "Move", this);
        battleState = new SkeletonBattleState(this,enemyStateMachine,"Move",this);
        skeletonAttack = new SkeletonAttack(this,enemyStateMachine,"Attack",this);
    }

    protected override void Start()
    {
        base.Start();

        enemyStateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
