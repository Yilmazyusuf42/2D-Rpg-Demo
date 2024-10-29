using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    #region Abilities
    public DashAbility dashAbility { get; private set; }
    public CloneAbility cloneAbility { get; private set; }

    #endregion

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        dashAbility = GetComponent<DashAbility>();
        cloneAbility = GetComponent<CloneAbility>();
    }

}
