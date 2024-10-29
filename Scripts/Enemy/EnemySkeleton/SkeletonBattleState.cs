using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    EnemySkeleton enemy;
    Transform player;
    int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        stateTimer = enemy.battleDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance)
        {
            if (enemy.CanAttack())
                enemy.enemyStateMachine.ChangeState(enemy.skeletonAttack);
            return;
        }

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.battleDistance)
        {
            Debug.Log(Vector2.Distance(player.transform.position, enemy.transform.position));
            enemyStateMachine.ChangeState(enemy.idleState);
        }

        if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }
        else if (player.position.x > enemy.transform.position.x)
            moveDir = 1;

        enemy.SetVelocity(enemy.speed * moveDir, enemy.rb.velocity.y);


    }
}
