using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) 
    : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2 (rb.velocity.x, player.jumpForce);
    }
    public override void Update()
    {
        base.Update();
        if(!player.IsGroundDetected())
            stateMachine.ChangeState(player.playerAirState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
