using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordSkillController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Collider2D cd;
    Player player;



    //! Sword Returning İnfos
    bool canRotate = true;
    private float returningSpeed = 12f;
    bool IsReturning;
    private float freezeDuration;
    private float maxDistance;


    //! Sword Bouncing 
    [Header("Sword Type bouncing infos")]
    bool isBouncing;
    int amountOfBouncing;
    float swordBouncingSpeed;
    int bouncingIndex;
    List<Transform> enemyList = new List<Transform>();

    //! Sword Piercing
    [Header("Sword Type Piercing İnfos")]
    int amountPiercing;
    bool isPiercing;
    float piercingSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    //! Sword Type Spining
    [Header("Sword Type Spining infos")]
    private bool isSpining;
    private bool wasStoped = false;
    private float spinDuration;
    private float spinTimer;
    private float maxTravelDistance;
    private float hitTimer = 0;
    private float hitCoolDown;

    void Update()
    {
        // Apply the sword to gravity
        if (canRotate)
            transform.right = rb.velocity;

        // returning the sword to the character
        if (IsReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * returningSpeed);
            if (Vector2.Distance(player.transform.position, transform.position) < 1f)
                player.CatchTheSword();
        }

        BouncingLogic();
        SpiningLogic();
        CheckingDistance();
    }

    private void CheckingDistance()
    {
        if (maxDistance < Vector2.Distance(player.transform.position, transform.position))
        {
            Debug.Log("Destroy myself");
            Destroy(gameObject);
        }
    }


    public void SetupSword(Vector2 _launch, float _gravity, Player _player, float _freezeDuration, float _maxDistance)
    {
        freezeDuration = _freezeDuration;
        maxDistance = _maxDistance;
        rb.velocity = _launch;
        rb.gravityScale = _gravity;

        if (amountPiercing <= 0)
            anim.SetBool("Flip", true);


        player = _player;
    }




    #region  SkillsLogic
    private void SpiningLogic()
    {
        if (isSpining)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStoped)
            {
                StopWhenSpining();
            }

            if (wasStoped)
            {
                spinTimer -= Time.deltaTime;
                hitTimer -= Time.deltaTime;
                if (spinTimer < 0)
                {
                    IsReturning = true;
                    wasStoped = false;
                }

                if (hitTimer < 0)
                {
                    Debug.Log("Tespit edeyrum");

                    hitTimer = hitCoolDown;
                    Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, 1f);
                    foreach (var item in collider2D)
                    {
                        if (item.GetComponent<Enemy>() != null)
                        {
                            Enemy enemy = item.GetComponent<Enemy>();
                            SkillDamagedEnemy(enemy);
                        }
                    }
                }
            }
        }
    }

    private void StopWhenSpining()
    {
        wasStoped = true;
        spinTimer = spinDuration;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }


    private void BouncingLogic()
    {
        if (isBouncing && enemyList.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyList[bouncingIndex].position, Time.deltaTime * swordBouncingSpeed);

            float theDistance = Vector2.Distance(transform.position, enemyList[bouncingIndex].position);

            if (theDistance < .1f)
            {
                SkillDamagedEnemy(enemyList[bouncingIndex].GetComponent<Enemy>());
                bouncingIndex++;
                amountOfBouncing--;
                if (amountOfBouncing <= 0)
                {
                    isBouncing = false;
                    IsReturning = true;
                }
                if (bouncingIndex >= enemyList.Count)
                    bouncingIndex = 0;
            }
        }
    }


    public void ReturningSword()
    {
        IsReturning = true;
        //rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;

    }

    #endregion


    #region SetupSkills
    public void SetupBouncing(int _amountOfBouncing, bool _isBouncing, float _bouncingSpeed)
    {
        amountOfBouncing = _amountOfBouncing;
        isBouncing = _isBouncing;
        swordBouncingSpeed = _bouncingSpeed;
    }

    public void SetupPierce(int _amountOfPierce, bool _isPiercing, float _piercingSpeed)
    {
        amountPiercing = _amountOfPierce;
        isPiercing = _isPiercing;
        piercingSpeed = _piercingSpeed;
    }

    public void SetupSpining(bool _isSpining, float _spinDuration, float _maxTravelDistance, float _hitCoolDown)
    {
        isSpining = _isSpining;
        spinDuration = _spinDuration;
        maxTravelDistance = _maxTravelDistance;
        hitCoolDown = _hitCoolDown;
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsReturning)
            return;


        if (other.GetComponent<Enemy>() != null)
            if (enemyList.Count <= 0 && isBouncing)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15f);
                foreach (var item in colliders)
                {
                    if (item.GetComponent<Enemy>() != null)
                        enemyList.Add(item.transform);
                }
                if (enemyList.Count > 1)
                    isBouncing = true;

            }


        StuckTheSword(other);
    }

    private void StuckTheSword(Collider2D other)
    {
        if (amountPiercing > 0 && other.GetComponent<Enemy>())
        {
            amountPiercing--;
            SkillDamagedEnemy(other.GetComponent<Enemy>());
            return;
        }
        if (isSpining)
        {
            StopWhenSpining();
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyList.Count > 0)
            return;
        transform.parent = other.transform;
        SkillDamagedEnemy(other.GetComponent<Enemy>());

        anim.SetBool("Flip", false);
        
    }

    

    private void SkillDamagedEnemy(Enemy other)
    {
        if (other == null)
            return;
        other.Damaged();
        other.StartCoroutine("FreezingFor", freezeDuration);
    }
}
