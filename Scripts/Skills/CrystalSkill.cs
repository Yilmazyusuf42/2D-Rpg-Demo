using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameObject crystelPrefab;
    int originalMultiCrystalAmount;
    GameObject currentCrystal;

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
            currentCrystal = Instantiate(crystelPrefab, player.transform.position, Quaternion.identity);

            CrystelSkillController crystalScript = currentCrystal.GetComponent<CrystelSkillController>();
            crystalScript.SetupCrystelSkill(duration, canExplode, canRoam, roamSpeed, GetClosestEnemy(currentCrystal.transform));

        }
        else
        {
            if (currentCrystal.GetComponent<CrystelSkillController>().RoamStatus())
                return;
            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;
            currentCrystal.GetComponent<CrystelSkillController>().FinishCrystal();

        }

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
                SetupCrystelSkill(duration, canExplode, canRoam, roamSpeed, GetClosestEnemy(lastMultiCrystal.transform));
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
            SetupCrystelSkill(duration, canExplode, canRoam, roamSpeed, GetClosestEnemy(lastMultiCrystal.transform));
            currentMultiCrystal--;
            yield return new WaitForSeconds(gapBetween);

        }
        Cooldown = multiCrystalCoolDown;
    }
    */
}
