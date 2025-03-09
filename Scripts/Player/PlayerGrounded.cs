using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : PlayerState
{
    public PlayerGrounded(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();


        if (Input.GetKeyDown(KeyCode.R) && player.skill.blackhole_Skill.CanUseBlackhole())
            stateMachine.ChangeState(player.playerBlacholeState);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasSword())
        {
            if (xInput != 0)
                player.playerAimSwordState.fromMovingState = true;
            stateMachine.ChangeState(player.playerAimSwordState);

        }

        if (Input.GetKeyDown(KeyCode.E))
            stateMachine.ChangeState(player.playerCounterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.playerPrimaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.playerAirState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.playerJump);
    }
    bool HasSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<SwordSkillController>().ReturningSword();
        return false;

    }


}
