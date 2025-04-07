using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float Cooldown;
    public float coolDownTimer;
    protected Player player;

    public virtual void Start()
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
        float closestDistance = Mathf.Infinity;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_currentPos.transform.position, 15f);

        foreach (var hit in enemies)
        {
            Debug.Log(hit.transform.position.x);
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(_currentPos.transform.position, hit.transform.position);
                if (distance < closestDistance && hit.GetComponent<Enemy>().IsDead() != true)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }

        }
        Debug.Log(closestEnemy);
        return closestEnemy;

    }
}
