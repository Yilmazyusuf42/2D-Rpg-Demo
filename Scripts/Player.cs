using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [Header("Movement Attribiutes")]
    public float speed = 8f;

    public Animator playerAnim { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public Rigidbody2D playerRb {get; private set;}

    private void Awake() {
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,playerStateMachine,"Idle");
        moveState = new PlayerMoveState(this,playerStateMachine,"Move");
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        playerAnim = GetComponentInChildren<Animator>();
        playerStateMachine.Initialize(idleState);
    }
    

    private void Update() {
        playerStateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) {
        playerRb.velocity = new Vector2 (_xVelocity, _yVelocity);
    }
        
    }
