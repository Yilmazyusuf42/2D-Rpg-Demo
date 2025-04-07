
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
    sbyte enemyFaceDir = 1;
    private float? closesDistance;
    bool isMirrorCloneCreated = false;
    bool canCrateMultipleClone;
    Transform closestEnemy;
    Player player;
    int chanceofMultipleClone = 40;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("my initali transform" + transform.position);
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

    public void SetupClone(Transform _transform, float _cloneDuration, Vector3 ofset, Transform _closestEnemy, bool _canCreateMultiClone, Player _player)
    {
        player = _player;
        closestEnemy = _closestEnemy;
        canCrateMultipleClone = _canCreateMultiClone;
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

    // isMirrorCreated for stop creating 2 mirror in same place if there is 2 overlapping enemy

    private void TriggerAttackDamage()
    {
        var enemies = Physics2D.OverlapCircleAll(attackCheck.transform.position, attackRad);

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
                player.stats.DoDamage(enemy.GetComponent<CharacterStats>());

            if (canCrateMultipleClone && enemy.GetComponent<Enemy>() != null)
            {

                if (Random.Range(0, 100) < chanceofMultipleClone && !isMirrorCloneCreated)
                {
                    SkillManager.instance.cloneAbility.CreateClone(enemy.transform, new Vector3(0.5f * enemyFaceDir, 0));
                    isMirrorCloneCreated = true;
                }
            }
        }
    }

    private void FaceClosestEnemy()
    {


        if (closestEnemy != null)
        {
            if (closestEnemy.transform.position.x < transform.position.x)
            {
                enemyFaceDir *= -1;
                transform.Rotate(new Vector3(0, 180, 0));
                Debug.Log("döndüm");
            }
        }
    }
}
