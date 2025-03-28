
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LightningController : MonoBehaviour
{
    public CharacterStats target;
    public float speed;
    Animator anim;
    int damage;
    bool triggered;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;

        if (triggered)
            return;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, target.transform.position);
        transform.right = transform.position - target.transform.position;

        if (distance < .1f)
        {
            anim.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one * 3;
            target.TakingDamage(5);
            triggered = true;
            Invoke("HitandDestroyself", .2f);
        }


    }

    public void SetupLightning(CharacterStats _target, int _damage)
    {
        damage = _damage;
        target = _target;

    }
    void HitandDestroyself()
    {
        target.TakingDamage(damage);
        target.ApplyShocking(true);
        anim.SetTrigger("Hit");
        Destroy(gameObject, .4f);
    }
}
