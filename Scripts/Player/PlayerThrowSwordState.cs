using UnityEngine;

public class PlayerThrowSwordState : PlayerState
{
    public PlayerThrowSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("fırlattım");
    }


}