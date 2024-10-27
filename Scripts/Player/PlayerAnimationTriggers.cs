using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    private void TriggerTheFinishedAnimation()
    {
        player.AnimationTrigger();
    }

    private void TriggerAttackDamage()
    {
        var enemies = Physics2D.OverlapCircleAll(player.AttackInfos().Item1, player.AttackInfos().Item2);

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.Damaged();
        }
    }
}
