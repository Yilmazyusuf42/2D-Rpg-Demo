using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float Cooldown;
    float coolDownTimer;


    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public bool CanUseSkill()
    {
        if (coolDownTimer < 0)
        {
            UseSkill();
            coolDownTimer = Cooldown;
            return true;
        }
        Debug.Log($"{this}  is on CoolDown");
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
