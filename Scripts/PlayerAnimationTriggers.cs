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
}
