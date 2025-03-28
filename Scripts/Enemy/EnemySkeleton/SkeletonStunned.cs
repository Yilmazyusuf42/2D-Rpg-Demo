using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunned : EnemyState
{
    EnemySkeleton enemy;
    public SkeletonStunned(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.rb.velocity = new Vector2(enemy.stunnedShaking.x * -enemy.facingDir, enemy.stunnedShaking.y);
        enemy.entityFx.InvokeRepeating("RedBlink", 0f, .2f);
        stateTimer = enemy.stunnedDuration;
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            enemyStateMachine.ChangeState(enemy.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.ClosePerryIcon();
        enemy.entityFx.Invoke("CancelColorChange", 0f);
    }
}
