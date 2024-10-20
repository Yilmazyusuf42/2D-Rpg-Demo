using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : PlayerState
{
    public PlayerWallSlide(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.playerWallJump);
            return;
        }

        if(yInput > 0 )
            rb.velocity = new Vector2(rb.velocity.x, -player.wallSlideSpeed);
        else
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);



        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        Debug.Log($"FAcing dir => {player.facingDir} \n Input Direction => {xInput}");
    }
}
