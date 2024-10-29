using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneAbility : Skills
{
    public GameObject playerClone;
    public float fadingSpeed;
    public float cloneDuration;
    public void CreateClone(Transform _transform, float duration)
    {
        CloneAbilityController newClone = Instantiate(playerClone).GetComponent<CloneAbilityController>();
        newClone.SetupClone(_transform, duration);
    }
}
