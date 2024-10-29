using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : Entity
{
    [Header("Movement Attribiutes")]
    public float speed = 8f;
    public float jumpForce = 12f;
    public float counterAttackDuration;

    [Header("Attack Movements")]
    public Vector2[] attackMovements;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }

    #region States
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState playerJump { get; private set; }
    public PlayerAirState playerAirState { get; private set; }
    public PlayerDashState playerDashState { get; private set; }
    public PlayerWallSlide playerWallSlide { get; private set; }
    public PlayerWallJumpState playerWallJump { get; private set; }

    public PlayerPrimaryAttackState playerPrimaryAttack { get; private set; }
    public PlayerCounterAttack playerCounterAttack { get; private set; }
    #endregion
    public bool isBusy { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        playerJump = new PlayerJumpState(this, playerStateMachine, "Jump");
        playerAirState = new PlayerAirState(this, playerStateMachine, "Jump");
        playerDashState = new PlayerDashState(this, playerStateMachine, "Dash");
        playerWallSlide = new PlayerWallSlide(this, playerStateMachine, "WallSlide");
        playerWallJump = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        playerPrimaryAttack = new PlayerPrimaryAttackState(this, playerStateMachine, "Attack");
        playerCounterAttack = new PlayerCounterAttack(this, playerStateMachine, "CounterAttack");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        playerStateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        playerStateMachine.currentState.Update();
        CheckingForDash();
    }


    public void AnimationTrigger() => playerStateMachine.currentState.AnimationFinishTrigger();

    private void CheckingForDash()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dashAbility.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            playerStateMachine.ChangeState(playerDashState);

        }

    }
    public IEnumerator IsBusy(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }







}
