using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGrounded
{
    public PlayerMoveState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.speed, rb.velocity.y);

        if(xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit(){
        base.Exit();
        
    }

}
