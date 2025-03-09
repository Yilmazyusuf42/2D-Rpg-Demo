using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float Cooldown;
    public float coolDownTimer;
    protected Player player;

    void Start()
    {
        player = PlayerManager.instance.player;

    }
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

    protected virtual Transform GetClosestEnemy(Transform _currentPos)
    {
        Transform closestEnemy = null;
        float? closestDistance = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_currentPos.transform.position, 25);

        foreach (var hit in enemies)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(_currentPos.transform.position, hit.transform.position);
                if (distance < closestDistance || closestDistance == null)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }

        }

        return closestEnemy;

    }
}
