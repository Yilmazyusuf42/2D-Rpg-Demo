using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneAbility : Skills
{
    public GameObject playerClone;
    public float fadingSpeed;
    public float cloneDuration;


    [SerializeField] private bool canCreateCloneDashStart;
    [SerializeField] private bool canCreateCloneDashEnd;
    [SerializeField] private bool canCreateCloneCounterAttack;


    public void CreateClone(Transform _transform, float duration)
    {
        CloneAbilityController newClone = Instantiate(playerClone).GetComponent<CloneAbilityController>();
        newClone.SetupClone(_transform, duration, Vector3.zero, GetClosestEnemy(newClone.transform));
    }
    // Here default duration is 1f and there is ofset
    public void CreateClone(Transform _transform, Vector3 ofset)
    {
        CloneAbilityController newClone = Instantiate(playerClone).GetComponent<CloneAbilityController>();
        newClone.SetupClone(_transform, 1f, ofset, GetClosestEnemy(newClone.transform));
    }




    public void CreateCloneDashStart()
    {
        if (canCreateCloneDashStart)
            player.skill.cloneAbility.CreateClone(player.transform, player.skill.cloneAbility.cloneDuration);
    }

    public void CreateCloneDashEnd()
    {
        if (canCreateCloneDashEnd)
            player.skill.cloneAbility.CreateClone(player.transform, player.skill.cloneAbility.cloneDuration);
    }


    public void CreateCloneCounterAttack(Transform _enemy)
    {
        if (canCreateCloneCounterAttack)
            StartCoroutine(CreateCounterAttackClone(_enemy, new Vector3(2 * player.facingDir, 0)));

    }

    IEnumerator CreateCounterAttackClone(Transform _transform, Vector3 _ofset)
    {
        yield return new WaitForSeconds(0.4f);
        player.skill.cloneAbility.CreateClone(_transform, _ofset);

    }



}
