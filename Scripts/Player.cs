using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [Header("Movement Attribiutes")]
    public float speed = 8f;
    public float jumpForce = 12f;

    [Header("Dash infos")]
    public float dashSpeed;
    public float dashDir { get; private set; }
    private float dashTimer;
    [SerializeField]private float dashCoolDown = 3f;
    public float dashDuration = .4f;
    

    [Header("Colission Info")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance;
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    #region States
    public Animator playerAnim { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public Rigidbody2D playerRb {get; private set;}
    public PlayerJumpState playerJump{ get; private set; }
    public PlayerAirState   playerAirState{ get; private set; }
    public PlayerDashState playerDashState { get; private set; }

    #endregion
    public  int facingDir{get; private set;} = 1;
    private bool facingRight = true;
    private void Awake() {
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,playerStateMachine,"Idle");
        moveState = new PlayerMoveState(this,playerStateMachine,"Move");
        playerJump = new PlayerJumpState(this,playerStateMachine,"Jump");
        playerAirState = new PlayerAirState(this,playerStateMachine,"Jump");
        playerDashState = new PlayerDashState(this,playerStateMachine,"Dash");
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        playerAnim = GetComponentInChildren<Animator>();
        playerStateMachine.Initialize(idleState);
    }
    

    private void Update() {
        playerStateMachine.currentState.Update();
        CheckingForDash();
    }

    private void CheckingForDash()
    {
        dashTimer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            playerStateMachine.ChangeState(playerDashState);

        }
        
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) {
        playerRb.velocity = new Vector2 (_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }


    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance,whatIsGround);

    private  void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip() 
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void FlipController(float _x)
    {
        if(_x > 0 && !facingRight)
            Flip();
        else if(_x < 0 && facingRight)
            Flip();
    }

        
    }
