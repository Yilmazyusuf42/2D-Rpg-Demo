using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalSkill : Skills
{

    [SerializeField] private float duration;
    [SerializeField] private bool canExplode;
    [SerializeField] private bool canRoam;
    [SerializeField] private float roamSpeed;

    [Space(10)]
    [SerializeField] private bool canMultiCrystal;
    [SerializeField] private int multiCrystalAmount;
    [SerializeField] private float multiCrystalCoolDown;
    // private float gapBetween = 0.1f;
    [SerializeField] private bool canLeaveCopySelf;
    [SerializeField] private GameObject crystelPrefab;
    int originalMultiCrystalAmount;
    GameObject currentCrystal;
    [SerializeField] private LayerMask enemyTypeMask;

    private void Awake()
    {
        originalMultiCrystalAmount = multiCrystalAmount;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log($"calistim {multiCrystalAmount} ");

        if (CanUseCrystalAbility())
            return;

        if (currentCrystal == null)
        {
            CreateCrystal();

        }
        else
        {
            if (currentCrystal.GetComponent<CrystelSkillController>().RoamStatus())
                return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = playerPos;

            if (canLeaveCopySelf)
            {
                player.skill.cloneAbility.CreateClone(currentCrystal.transform, 0);
                Destroy(currentCrystal);
                return;
            }


            currentCrystal.GetComponent<CrystelSkillController>().FinishCrystal();
        }

    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystelPrefab, player.transform.position, Quaternion.identity);

        CrystelSkillController crystalScript = currentCrystal.GetComponent<CrystelSkillController>();
        crystalScript.SetupCrystelSkill(duration, canExplode, canRoam, canLeaveCopySelf, roamSpeed, GetClosestEnemy(currentCrystal.transform), enemyTypeMask, player);

        if (SkillManager.instance.cloneAbility.crystalInsteadofClone)
            crystalScript.ChooseRandomEnemy();


    }

    bool CanUseCrystalAbility()
    {
        if (canMultiCrystal)
        {
            if (multiCrystalAmount > 0)
            {
                Cooldown = 0;

                GameObject lastMultiCrystal = Instantiate(crystelPrefab, player.transform.position, Quaternion.identity);
                lastMultiCrystal.GetComponent<CrystelSkillController>().
                SetupCrystelSkill(duration, canExplode, canRoam, canLeaveCopySelf, roamSpeed, GetClosestEnemy(lastMultiCrystal.transform), enemyTypeMask, player);
                multiCrystalAmount--;
                if (multiCrystalAmount <= 0)
                {
                    Cooldown = multiCrystalCoolDown;
                    multiCrystalAmount = originalMultiCrystalAmount;
                }
            }
            return true;

        }


        return false;

    }




    /* if needed to be creating repeatedly

    bool CanUseCrystalAbility()
    {
        if (canMultiCrystal)
        {
            StartCoroutine(CreatingMultiCrystal());
            return true;
        }
        return false;


    }


    IEnumerator CreatingMultiCrystal()
    {
        int currentMultiCrystal = multiCrystalAmount;

        while (0 < currentMultiCrystal)
        {
            Debug.Log("calistim");
            GameObject lastMultiCrystal = Instantiate(crystelPrefab, player.transform.position, Quaternion.identity);
            lastMultiCrystal.GetComponent<CrystelSkillController>().
            SetupCrystelSkill(duration, canExplode, canRoam,canLeaveCopySelf,roamSpeed, GetClosestEnemy(lastMultiCrystal.transform));
            currentMultiCrystal--;
            yield return new WaitForSeconds(gapBetween);

        }
        Cooldown = multiCrystalCoolDown;
    }
    */
}
