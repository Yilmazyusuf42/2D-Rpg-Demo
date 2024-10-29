using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = SkillManager.instance.dashAbility.dashDuration;
        player.skill.cloneAbility.CreateClone(player.transform, player.skill.cloneAbility.cloneDuration);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.playerWallSlide);
            return;
        }

        player.SetVelocity(SkillManager.instance.dashAbility.dashSpeed * player.dashDir, rb.velocity.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);


    }

}
