using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(-player.facingDir * 5, player.jumpForce);
        player.FlipController(player.facingDir * -1);
        stateTimer = .4f;
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        stateTimer -= Time.deltaTime;

        if (stateTimer < 0)
            stateMachine.ChangeState(player.playerAirState);
    }

}
