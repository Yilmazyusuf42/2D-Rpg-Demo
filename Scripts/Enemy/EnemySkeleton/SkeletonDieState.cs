using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDieState : EnemyState
{
    EnemySkeleton enemy;
    public SkeletonDieState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0, -15);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
