using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }
    private float timeBox =2f;
    private float lastTimeAttacked;
    private int comboCounter;


    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + timeBox)
            comboCounter = 0;
        player.anim.SetInteger("AttackCombo", comboCounter);

        float attackDir = player.facingDir;
        if(xInput !=0)
            attackDir = xInput;


        player.SetVelocity(
            player.attackMovements[comboCounter].x * attackDir,
            player.attackMovements[comboCounter].y);

        stateTimer =.1f;
    }

    public override void Exit()
    {
        base.Exit();
        lastTimeAttacked = Time.time;
        comboCounter++;
        player.StartCoroutine("IsBusy", .15f);
        
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            rb.velocity = Vector2.zero;

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
