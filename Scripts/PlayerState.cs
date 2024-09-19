using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected float xInput,yInput;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;
    protected Rigidbody2D rb;
    
    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName){
        player = _player;
        stateMachine = _playerStateMachine;
        animBoolName = _animBoolName;
    }


    public virtual void Enter(){
        player.playerAnim.SetBool(animBoolName, true);
        rb = player.GetComponent<Rigidbody2D>();
    }

    public virtual void Update(){
        xInput = Input.GetAxisRaw("Horizontal");
    }

    public virtual void Exit(){
        player.playerAnim.SetBool(animBoolName, false);
    }

}
