using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerIdleState : PlayerGrounded
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0,0);
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        if(xInput != 0 && !player.isBusy)
        {
            player.SetVelocity(xInput,rb.velocity.y);
            player.playerStateMachine.ChangeState(player.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
