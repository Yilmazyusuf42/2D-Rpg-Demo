using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
    }
    public override void Update()
    {
        base.Update();
        
        //animation
        player.playerAnim.SetFloat("yVelocity", rb.velocity.y);

        //Wall detected
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.playerWallSlide);

        if(!player.IsGroundDetected() && xInput != 0)
        {
            rb.velocity = new Vector2(xInput * player.speed, rb.velocity.y);
            player.FlipController(xInput);
        }


        //Ground Detected
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
    public override void Exit()
    {
        base.Exit();
    }
}
