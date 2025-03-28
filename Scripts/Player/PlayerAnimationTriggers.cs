
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

            CharacterStats target = enemy.GetComponent<CharacterStats>();
            player.stats.DoDamage(target);
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }
}
