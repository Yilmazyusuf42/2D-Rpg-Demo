using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;
    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakingDamage(int _damage)
    {
        base.TakingDamage(_damage);

        player.Damaged();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }

}
