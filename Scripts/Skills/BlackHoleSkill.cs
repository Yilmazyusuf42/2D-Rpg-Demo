using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skills
{
    [SerializeField] private GameObject BlackholeSkillPrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float scaleSpeed;
    [SerializeField] private int attackAmount;
    [SerializeField] private float cloneCoolDown;
    [SerializeField] private float blackholeDuration;



    public override void UseSkill()
    {
        base.UseSkill();
        GameObject blackholePref = Instantiate(BlackholeSkillPrefab, player.transform.position, Quaternion.identity);

        BlackHoleSkill_Controller blackholeSkill = blackholePref.GetComponent<BlackHoleSkill_Controller>();
        blackholeSkill.BlackHoleSetup(maxSize, scaleSpeed, attackAmount, cloneCoolDown, blackholeDuration);
    }


    protected override void Update()
    {
        base.Update();

    }

    public bool CanUseBlackhole()
    {
        if (coolDownTimer < 0)
            return true;
        else
        {
            Debug.Log("Blackhole dinleniyor");
            return false;
        }
    }

}
