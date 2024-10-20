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

}
