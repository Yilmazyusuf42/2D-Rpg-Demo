using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;

        if (sword.position.x < player.transform.position.x && player.facingDir == 1)
            player.Flip();
        else if (sword.position.x > player.transform.position.x && player.facingDir == -1)
            player.Flip();

        rb.velocity = new Vector2(-player.facingDir * player.swordReturningImpact, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }
}
