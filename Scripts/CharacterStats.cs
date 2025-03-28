using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Major Stats")]
    public Stats strength;
    public Stats agility;
    public Stats vitality;
    public Stats intelligence;


    [Header("Defensive Stats")]
    public Stats maxHealt;
    public Stats armor;
    public Stats evesion;
    public Stats magicalDefanse;

    [Header("Offansive Stats")]
    public Stats damage;
    public Stats critChance;
    public Stats critDamage;

    [Header("Magic Stats")]
    public Stats firedamage;
    public Stats waterDamage;
    public Stats lightningDamage;


    public int currentHealth;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;


    float ignitedTimer;
    float igniteAttackCoolDown = .3f;
    float igniteAttackTimer;

    float chillTimer;
    float chillAttackCoolDown = 0.3f;
    float chillAttackTimer;

    float lightenedTimer;
    float lightenedAttackCoolDown = 0.3f;
    float lightenedAttackTimer;
    [SerializeField] private GameObject lightningStrike;
    int lightStrikeDamage;
    int igniteDamage;
    int chilledDamage;
    int shockedDamage;

    public System.Action UpdateHealthBar;
    EntityFx fx;




    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        critDamage.BaseValue(150);
        igniteAttackTimer = igniteAttackCoolDown;
        fx = GetComponentInChildren<EntityFx>();
    }


    protected virtual void Update()
    {
        igniteAttackTimer -= Time.deltaTime;
        ignitedTimer -= Time.deltaTime;

        chillAttackTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;

        lightenedAttackTimer -= Time.deltaTime;
        lightenedTimer -= Time.deltaTime;



        if (igniteAttackTimer < 0 && isIgnited)
        {
            igniteAttackTimer = igniteAttackCoolDown;
            Debug.Log("verdim atesi " + igniteDamage);
            currentHealth -= igniteDamage;

            if (currentHealth <= 0)
                Die();

        }

        if (ignitedTimer < 0 && isIgnited)
        {
            isIgnited = false;
        }

        if (chillAttackTimer < 0 && isChilled)
        {
            chillAttackTimer = chillAttackCoolDown;
            Debug.Log("verdim sogugu");
            currentHealth -= chilledDamage;

            if (currentHealth <= 0)
                Die();
        }

        if (chillTimer < 0 && isChilled)
        {
            isChilled = false;

        }



        if (lightenedAttackTimer < 0 && isShocked)
        {
            lightenedAttackTimer = lightenedAttackCoolDown;
            currentHealth -= shockedDamage;

            if (currentHealth <= 0)
                Die();
            Debug.Log("verdim elektrigi");
        }

        if (lightenedTimer < 0 && isShocked)
        {
            isShocked = false;

        }

    }

    public void DoDamage(CharacterStats _target)
    {


        int totalDamage = CallculateDamage(_target);

        if (CanMissAttack(_target))
            return;

        if (CanCriticAttack())
        {
            totalDamage = CalculatingCriticDamage(totalDamage);
            Debug.Log("critic damage " + totalDamage);
        }



        _target.TakingDamage(totalDamage);
        //DoMagicalDamage(_target);
    }



    public virtual void TakingDamage(int _damage)
    {
        DecreaseHealth(_damage);
        if (currentHealth <= 0)
            Die();

        Debug.Log($"i taking damed {_damage} and my current health {currentHealth}");
    }




    public virtual void DoMagicalDamage(CharacterStats _target)
    {

        int _fireDamage = firedamage.GetValue();
        int _waterDamage = waterDamage.GetValue();
        int _lightningDamage = lightningDamage.GetValue();

        bool canApplyIgnite = _fireDamage > _waterDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _waterDamage > _fireDamage && _waterDamage > _lightningDamage;
        bool canApplyLighting = _lightningDamage > _fireDamage && _lightningDamage > _waterDamage;

        if (Mathf.Max(_fireDamage, _lightningDamage, _waterDamage) <= 0)
            return;


        Debug.Log("befor de while" + canApplyIgnite);

        // if everySpecial attack has same value bigger than zero, it going to be random selected
        while (!canApplyChill && !canApplyIgnite && !canApplyLighting)
        {
            if (Random.value < 0.3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _target.ApplyAilments(canApplyChill, canApplyLighting, canApplyIgnite);
                Debug.Log("ates uygulayabailirim");
                break;
            }

            if (Random.value < 0.3f && _waterDamage > 0)
            {
                canApplyChill = true;
                _target.ApplyAilments(canApplyChill, canApplyLighting, canApplyIgnite);
                Debug.Log("soguk uygulayabilirim");
                break;
            }

            if (Random.value < 0.3f && _lightningDamage > 0)
            {
                canApplyLighting = true;
                _target.ApplyAilments(canApplyChill, canApplyLighting, canApplyIgnite);
                Debug.Log("ceyran uygulayabilirim");
                break;
            }
        }

        int totalMagicalDamage = _fireDamage + _waterDamage + _lightningDamage + intelligence.GetValue();
        totalMagicalDamage = CalculatingResistance(_target, totalMagicalDamage);
        _target.TakingDamage(totalMagicalDamage);


        if (canApplyIgnite)
        {
            int igniteValue = Mathf.RoundToInt(_fireDamage * .2f);
            Debug.Log("ayarladim atesi " + igniteValue);
            _target.SetupIgnitedDamage(igniteValue);

        }

        if (canApplyChill)
        {
            int chilValue = Mathf.RoundToInt(_waterDamage * .2f);
            Debug.Log("ayarladim sogugu " + chilValue);
            _target.SetupChilledDamage(chilValue);
        }

        if (canApplyLighting)
        {
            int lightStrikeDamageValue = Mathf.RoundToInt(_lightningDamage * .4f);
            int lightValue = Mathf.RoundToInt(_lightningDamage * .2f);
            Debug.Log("ayarladim elektrigi " + shockedDamage);
            _target.SetupLightningDamage(lightValue);
            _target.SetupLightningStrikeDamage(lightStrikeDamageValue);
        }

        if (canApplyChill || canApplyLighting || canApplyIgnite)
        {
            Debug.Log($"chill {canApplyChill} light {canApplyLighting} ignite {canApplyIgnite}");
            _target.ApplyAilments(canApplyChill, canApplyLighting, canApplyIgnite);
            Debug.Log("bir şey uygulaadım ben haa");
        }
        else
            Debug.Log("ben hiç bir şey uygulaamıyorum beceriksiz bir herifim.");
    }

    private static int CalculatingResistance(CharacterStats _target, int totalMagicalDamage)
    {
        totalMagicalDamage -= _target.magicalDefanse.GetValue() + (_target.intelligence.GetValue() * 3);

        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public virtual void ApplyAilments(bool _isChilled, bool _isShocked, bool _isIgnited)
    {
        bool canApplyIgnite = !isChilled && !isIgnited && !isShocked;
        bool canApplyChill = !isChilled && !isIgnited && !isShocked;
        bool canApplyShock = !isChilled && !isIgnited;


        if (_isShocked && canApplyShock)
        {
            if (!isShocked)
            {
                ApplyShocking(_isShocked);
            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;


                ApplyLightningStrike();

            }
            Debug.Log("shock uygulamaya çalışıyorum gibi");
        }


        if (_isChilled && canApplyChill)
        {
            chillTimer = 2f;
            fx.ChillingFor(chillTimer);

            float slowPercentage = 0.4f;
            float slowDuration = chillTimer;
            GetComponent<Entity>().SlowMotion(slowPercentage, slowDuration);

        }
        if (_isIgnited && canApplyIgnite)
        {
            ignitedTimer = 2f;
            fx.IgniteFor(ignitedTimer);
        }


        isIgnited = _isIgnited;
        isChilled = _isChilled;
    }

    public void ApplyShocking(bool _isShocked)
    {
        if (isShocked)
            return;

        lightenedTimer = 2f;
        fx.ShockingFor(lightenedTimer);
        isShocked = _isShocked;
    }

    private void ApplyLightningStrike()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 25);

        foreach (var hit in enemies)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance && distance > 3f)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }

        }


        if (closestEnemy != null)
        {
            Instantiate(lightningStrike, transform.position, Quaternion.identity).
            GetComponent<LightningController>().
            SetupLightning(closestEnemy.GetComponent<CharacterStats>(), lightStrikeDamage);
        }
    }

    public virtual void SetupIgnitedDamage(int _damage) => igniteDamage = _damage;
    public virtual void SetupChilledDamage(int _damage) => chilledDamage = _damage;
    public virtual void SetupLightningDamage(int _damage) => shockedDamage = _damage;
    public virtual void SetupLightningStrikeDamage(int _damage) => lightStrikeDamage = _damage;
    void DecreaseHealth(int _damage)
    {
        currentHealth -= _damage;
        UpdateHealthBar();
    }


    protected virtual void Die()
    {
        Debug.Log("you are died bitch");
    }
    private bool CanMissAttack(CharacterStats _target)
    {
        int missAttackChange = _target.agility.GetValue() + _target.evesion.GetValue();

        //if has lightned missAttack decrease 20 
        if (_target.isShocked)
            missAttackChange -= 20;

        if (missAttackChange >= Random.Range(0, 100))
        {
            Debug.Log("ıskaladın");
            return true;
        }
        return false;

    }

    private bool CanCriticAttack()
    {
        int totalCriticChance = (critChance.GetValue() + agility.GetValue());

        if (Random.Range(0, 100) < critChance.GetValue())
        {
            return true;
        }
        return false;
    }

    int CalculatingCriticDamage(int _damage)
    {
        float totalCritPower = (critDamage.GetValue() + strength.GetValue()) * .01f;
        float totalCritdamage = totalCritPower * _damage;
        return Mathf.RoundToInt(totalCritdamage);
    }


    public int GetMaxHealthValue()
    {
        return maxHealt.GetValue() + vitality.GetValue() * 5;
    }

    private int CallculateDamage(CharacterStats _target)
    {
        int totalDamage;

        // if has chilled, armor decrease %20
        if (_target.isChilled)
            totalDamage = damage.GetValue() - Mathf.RoundToInt(_target.armor.GetValue() * .8f);
        else
            totalDamage = damage.GetValue() - _target.armor.GetValue();


        Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }


}
