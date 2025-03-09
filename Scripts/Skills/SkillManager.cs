using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    #region Abilities
    public DashAbility dashAbility { get; private set; }
    public CloneAbility cloneAbility { get; private set; }
    public SwordSkill swordSkill { get; private set; }
    public BlackholeSkill blackhole_Skill { get; private set; }
    public CrystalSkill crystalSkill { get; private set; }
    #endregion

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        dashAbility = GetComponent<DashAbility>();
        cloneAbility = GetComponent<CloneAbility>();
        swordSkill = GetComponent<SwordSkill>();
        blackhole_Skill = gameObject.GetComponent<BlackholeSkill>();
        crystalSkill = GetComponent<CrystalSkill>();
    }

}
