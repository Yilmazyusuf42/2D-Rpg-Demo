using UnityEngine;

public class CrystelSkillController : MonoBehaviour
{
    Animator anim => GetComponent<Animator>();
    CircleCollider2D damageArea => GetComponent<CircleCollider2D>();
    Transform closestEnemy;
    Player player;
    float skillTimer;
    bool canExplode;
    bool canRoam;
    bool canLeaveCopySelf;
    float roamSpeed;
    bool canGrow;
    float explodeSize = 5f;
    float growSpeed = 5f;
    LayerMask enemyType;



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

    public void FinishWithoutExplode() => DestroySelf();


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

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
                player.stats.DoMagicalDamage(enemy.GetComponent<CharacterStats>());

            Debug.Log("birisi var içimde " + enemy.name);
        }

    }

    public void SetupCrystelSkill(float _skillDuration, bool _canExplode, bool _canRoam, bool _canLeaveCopySelf, float _roamSpeed, Transform _closestEnemy, LayerMask _enemyTyep, Player _player)
    {
        player = _player;
        enemyType = _enemyTyep;
        canLeaveCopySelf = _canLeaveCopySelf;
        roamSpeed = _roamSpeed;
        closestEnemy = _closestEnemy;
        skillTimer = _skillDuration;
        canExplode = _canExplode;
        canRoam = _canRoam;

        if (closestEnemy == null)
            canRoam = false;
    }
    public void ChooseRandomEnemy()
    {
        float maxRange = SkillManager.instance.blackhole_Skill.RadiusofBlackhole();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, maxRange, enemyType);
        if (enemies.Length > 0)
        {
            closestEnemy = enemies[Random.Range(0, enemies.Length)].GetComponent<Transform>();
        }

    }

    public bool RoamStatus() => canRoam;

    void DestroySelf() => Destroy(gameObject);

}
