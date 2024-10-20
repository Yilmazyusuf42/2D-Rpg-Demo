using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState 
{

    Enemy enemyBase;
    string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState currentState;
    public EnemyStateMachine enemyStateMachine { get; private set; }



    public  EnemyState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName)
    {
        enemyBase = _enemyBase;
        animBoolName = _animBoolName;
        enemyStateMachine = _enemyStateMachine;
    }


    public virtual void Enter()
    {

        enemyBase.anim.SetBool(animBoolName,true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit() 
    { 
        enemyBase.anim.SetBool(animBoolName,false);
    }

    public virtual void AnimationFinished()
    {
        triggerCalled = true;
    }
}
