using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * SkillManager.instance.cloneAbility.fadingSpeed));
            Invoke("DestroyObject", 1f);
        }
    }

    void DestroyObject() => Destroy(gameObject);

    public void SetupClone(Transform _transform, float _cloneDuration, Vector3 ofset, Transform _closestEnemy)
    {
        closestEnemy = _closestEnemy;
        transform.position = _transform.position + ofset;
        timer = _cloneDuration;
        if (canAttack)
            anim.SetInteger("AttackCounter", Random.Range(1, 3));

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
        if (closestEnemy != null)
        {
            if (closestEnemy.transform.position.x < transform.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
