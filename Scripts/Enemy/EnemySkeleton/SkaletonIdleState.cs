using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaletonIdleState : SkaletonGrounded
{
    EnemySkeleton enemy;

    public SkaletonIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName, _enemy)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleWaitingTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            enemyStateMachine.ChangeState(enemy.moveState);
        }
    }
}
