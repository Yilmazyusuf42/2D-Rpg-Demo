using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CrystelSkillController : MonoBehaviour
{
    Animator anim => GetComponent<Animator>();
    CircleCollider2D damageArea => GetComponent<CircleCollider2D>();
    Transform closestEnemy;

    float skillTimer;
    bool canExplode;
    bool canRoam;
    float roamSpeed;
    bool canGrow;
    float explodeSize = 5f;
    float growSpeed = 5f;



    void Update()
    {
        skillTimer -= Time.deltaTime;

        if (canRoam)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, Time.deltaTime * roamSpeed);
            if (Vector2.Distance(transform.position, closestEnemy.transform.position) < 1)
            {
                Debug.Log("patladım patlaycam");
                canRoam = false;
                FinishCrystal();
            }

        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(explodeSize, explodeSize), Time.deltaTime * growSpeed);

        if (skillTimer < 0)
            FinishCrystal();

    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            anim.SetTrigger("Explode");
            canGrow = true;
        }
        else
            DestroySelf();
    }

    public void DamageAnimationTrigger()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, damageArea.radius);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.Damaged();
            Debug.Log("birisi var içimde " + enemy.name);
        }

    }

    public void SetupCrystelSkill(float _skillDuration, bool _canExplode, bool _canRoam, float _roamSpeed, Transform _closestEnemy)
    {
        roamSpeed = _roamSpeed;
        closestEnemy = _closestEnemy;
        skillTimer = _skillDuration;
        canExplode = _canExplode;
        canRoam = _canRoam;

        if (closestEnemy == null)
            canRoam = false;
    }

    public bool RoamStatus() => canRoam;

    void DestroySelf() => Destroy(gameObject);

}
