using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerState 
{
    protected float xInput,yInput;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;
    protected Rigidbody2D rb;
    protected float stateTimer;
    protected bool triggerCalled;
    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName){
        player = _player;
        stateMachine = _playerStateMachine;
        animBoolName = _animBoolName;
    }


    public virtual void Enter(){
        player.anim.SetBool(animBoolName, true);
        rb = player.GetComponent<Rigidbody2D>();
        triggerCalled = false;
    }

    public virtual void Update(){
        player.anim.SetFloat("yVelocity",rb.velocity.y);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit(){
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
