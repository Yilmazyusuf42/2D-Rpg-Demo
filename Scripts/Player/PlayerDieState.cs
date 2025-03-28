using UnityEngine;

public class PlayerDieState : PlayerState
{

    public PlayerDieState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
    }


    public override void Exit()
    {
        base.Exit();
    }
}
