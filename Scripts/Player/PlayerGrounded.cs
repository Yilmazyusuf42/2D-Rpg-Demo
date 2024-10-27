using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : PlayerState
{
    public PlayerGrounded(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.E))
            stateMachine.ChangeState(player.playerCounterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.playerPrimaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.playerAirState);

        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.playerJump);

    
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
