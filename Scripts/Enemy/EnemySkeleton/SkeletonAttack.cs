using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : EnemyState
{
    EnemySkeleton enemy;
    public SkeletonAttack(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("I Attacked");
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0,0);
        if (triggerCalled)
            enemyStateMachine.ChangeState(enemy.battleState);
    }
}
