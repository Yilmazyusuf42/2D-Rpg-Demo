using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;
    EnemySkeleton skeleton;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        skeleton = GetComponent<EnemySkeleton>();
    }

    public override void TakingDamage(int _damage)
    {
        base.TakingDamage(_damage);
        Debug.Log("buraya geldim gibi");
        enemy.Damaged();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }

}
