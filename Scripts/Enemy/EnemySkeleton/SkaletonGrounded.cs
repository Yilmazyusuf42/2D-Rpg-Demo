using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaletonGrounded : EnemyState
{
    EnemySkeleton enemy;
    public SkaletonGrounded(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
            enemyStateMachine.ChangeState(enemy.battleState);
    }
}
