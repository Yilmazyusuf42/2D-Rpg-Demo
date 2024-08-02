using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName){
        player = _player;
        stateMachine = _playerStateMachine;
        animBoolName = _animBoolName;
    }


    public virtual void Enter(){
        Debug.Log(animBoolName + "içine girmiş bulunmaktayım");
        player.playerAnim.SetBool(animBoolName, true);
    }

    public virtual void Update(){
        Debug.Log(animBoolName + "içinde kalıyor bulunmaktayım");
    }

    public virtual void Exit(){
        Debug.Log(animBoolName + " içinden çıkmış bulunmaktayım");
        player.playerAnim.SetBool(animBoolName, false);
    }

}
