using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDieState : EnemyState
{
    EnemySkeleton enemy;
    float destroyTime;
    public SkeletonDieState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        destroyTime = 2f;
    }

    public override void Update()
    {
        base.Update();
        destroyTime -= Time.deltaTime;

        if (destroyTime < 0)
            GameObject.Destroy(enemy.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
