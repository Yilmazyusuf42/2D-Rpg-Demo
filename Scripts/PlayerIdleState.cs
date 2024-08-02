using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if(xInput != 0)
        {
            if(xInput < 0)
                player.transform.rotation = new Quaternion(0, 180,0, 0);
            else
                player.transform.rotation = new Quaternion(0, 0,0, 0);

            player.SetVelocity(xInput,rb.velocity.y);
            player.playerStateMachine.ChangeState(player.moveState);

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
