using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CloneAbilityController : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;

    public GameObject attackCheck;
    private bool attackFinished;
    private float attackRad = 0.8f;
    private float timer;
    public bool canAttack = true;
    private float? closesDistance;
    Transform closestEnemy;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 || attackFinished)
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * SkillManager.instance.cloneAbility.fadingSpeed));
    }
    public void SetupClone(Transform _transform, float _cloneDuration)
    {
        transform.position = _transform.position;
        timer = _cloneDuration;
        if (canAttack)
            anim.SetInteger("AttackCounter", UnityEngine.Random.Range(1, 3));

        FaceClosestEnemy();
    }

    private void TriggerTheFinishedAnimation()
    {
        anim.SetInteger("AttackCounter", 0);
        attackFinished = true;
    }

    private void TriggerAttackDamage()
    {
        var enemies = Physics2D.OverlapCircleAll(attackCheck.transform.position, attackRad);

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.Damaged();
        }
    }

    private void FaceClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 25);

        foreach (var hit in enemies)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closesDistance || closesDistance == null)
                {
                    closesDistance = distance;
                    closestEnemy = hit.transform;
                }
            }

        }

        if (closestEnemy != null)
        {
            if (closestEnemy.transform.position.x < transform.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
