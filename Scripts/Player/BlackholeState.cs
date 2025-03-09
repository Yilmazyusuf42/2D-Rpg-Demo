using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BlackholeState : PlayerState
{
    bool skillIsUsed;
    float defulatGravity;
    float flyTime = .2f;
    public BlackholeState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        defulatGravity = rb.gravityScale;
        skillIsUsed = false;
        rb.gravityScale = 0;
        stateTimer = flyTime;
    }

    public override void Update()
    {
        base.Update();


        if (stateTimer > 0)
            rb.velocity = new Vector2(0f, 15f);


        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0f, -.1f);

            if (!skillIsUsed)
            {
                if (player.skill.blackhole_Skill.CanUseSkill())
                    skillIsUsed = true;
            }

            /// The exit is in the BlackholeController

        }
    }

    public override void Exit()
    {
        player.Dissappearing(false);
        base.Exit();
        rb.gravityScale = defulatGravity;
    }

}
