using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }
    public bool fromMovingState;
    public override void Enter()
    {
        base.Enter();
        player.skill.swordSkill.DotsActive(true);

        if (fromMovingState)
        {
            stateTimer = player.slideTime;
            fromMovingState = false;
        }

    }

    public override void Update()
    {
        if (stateTimer > 0)
        {
            player.SetVelocity(player.speed * stateTimer * player.facingDir, rb.velocity.y);
        }

        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.playerThrowSwordState);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < player.transform.position.x && player.facingDir == 1)
        {
            player.Flip();
        }

        else if (mousePos.x > player.transform.position.x && player.facingDir == -1)
        {
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("IsBusy", .3f);
    }
}

