using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCounterAttack : PlayerState
{
    bool counterAttackCloneCreated;

    public PlayerCounterAttack(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        counterAttackCloneCreated = false;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("CounterAttackSuccess", false);
    }

    public override void Update()
    {
        player.SetVelocity(0, 0);
        base.Update();
        var enemies = Physics2D.OverlapCircleAll(player.attackCircle.transform.position, player.attackCirclekRadius);
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<EnemySkeleton>()?.CanBeStunned() == true)
            {
                player.anim.SetBool("CounterAttackSuccess", true);
                if (!counterAttackCloneCreated)
                    CallCounterAttackClone(enemy);
                stateTimer = 2f;
            }
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void CallCounterAttackClone(Collider2D enemy)
    {
        counterAttackCloneCreated = true;
        player.skill.cloneAbility.CreateCloneCounterAttack(enemy.transform);

    }
}
