using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.U2D;
using UnityEngine;

public class Player : Entity
{
    [Header("Movement Attribiutes")]
    public float speed = 8f;
    public float jumpForce = 12f;
    public float counterAttackDuration;
    public float swordReturningImpact;


    [Header("Attack Movements")]
    public Vector2[] attackMovements;
    public float slideTime;

    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

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
    public PlayerAimSwordState playerAimSwordState { get; private set; }
    public PlayerCatchSwordState playerCatchSwordState { get; private set; }
    public PlayerThrowSwordState playerThrowSwordState { get; private set; }
    public BlackholeState playerBlacholeState { get; private set; }
    public PlayerDieState playerDieState { get; private set; }
    #endregion


    float defaultMoveSpeed;
    float defaultJumpForce;
    float defaultDashSpeed;


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
        playerAimSwordState = new PlayerAimSwordState(this, playerStateMachine, "AimSword");
        playerCatchSwordState = new PlayerCatchSwordState(this, playerStateMachine, "CatchingSword");
        playerThrowSwordState = new PlayerThrowSwordState(this, playerStateMachine, "ThrowSword");
        playerBlacholeState = new BlackholeState(this, playerStateMachine, "Jump");
        playerDieState = new PlayerDieState(this, playerStateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        playerStateMachine.Initialize(idleState);

        defaultMoveSpeed = speed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = skill.dashAbility.dashSpeed;
    }


    protected override void Update()
    {
        base.Update();
        playerStateMachine.currentState.Update();
        CheckingForDash();

        if (Input.GetKeyDown(KeyCode.F))
            skill.crystalSkill.CanUseSkill();
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

    public void AssingtheSword(GameObject _sword) => sword = _sword;

    public void CatchTheSword()
    {
        playerStateMachine.ChangeState(playerCatchSwordState);
        Destroy(sword);
        StartCoroutine("IsBusy", 0.2f);
    }

    public void ExitBlackholeAbility()
    {
        playerStateMachine.ChangeState(playerAirState);
    }


    public override void Die()
    {
        base.Die();

        playerStateMachine.ChangeState(playerDieState);
    }


    public override void SlowMotion(float _slowPercentage, float _slowDuration)
    {
        anim.speed *= (1 - _slowPercentage);
        speed *= (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        skill.dashAbility.dashSpeed *= (1 - _slowPercentage);
        Invoke("ReturnNormalMotion", _slowDuration);
        Debug.Log("bebe çalıştı");

    }
    protected override void ReturnNormalMotion()
    {
        base.ReturnNormalMotion();

        speed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        skill.dashAbility.dashSpeed = defaultDashSpeed;
    }


}
