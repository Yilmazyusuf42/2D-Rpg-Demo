using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonFinishTrigger : MonoBehaviour 
{
    EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();
    
    public void AnimationFinished()
    {
        enemy.AnimationFinishTrigger();
        
    }

    public void TriggerAttackDamage(){

        var player = Physics2D.OverlapCircle(enemy.AttackInfos().Item1, enemy.AttackInfos().Item2 );

        player?.GetComponent<Player>()?.Damaged(); 
    }

    void TriggerPerryOn() => enemy.ShowPerryIcon();
    void TriggerPerryOff() => enemy.ClosePerryIcon();

}
