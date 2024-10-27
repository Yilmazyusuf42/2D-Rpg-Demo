using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Entity : MonoBehaviour 
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFx entityFx{ get; private set; }

    [Header("Taking Damage Attributes")]
    protected bool isDamaged;
    [SerializeField]protected  Vector2 damageShaking; 
    [SerializeField]protected float takingDamageDuration;  // It's going to be 0.07f

    [Header("Stunend Variables")]
    public float stunnedDuration;
    public Vector2 stunnedShaking;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    [Header("WallSlide Speed")]
    public float wallSlideSpeed;
    public float wallSlideFriction;
    [Header("Colission Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected  LayerMask whatIsGround;
    [SerializeField] public Transform attackCircle;
    [SerializeField] public float attackCirclekRadius;
    


    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        entityFx = GetComponentInChildren<EntityFx>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    protected virtual void Update()
    {

    }

    



    #region Gizmos
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCircle.position, attackCirclekRadius);
    }
    #endregion

    #region Flip 
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if(isDamaged) 
            return;
        

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void Damaged(){
        Debug.Log(gameObject.name + "  damaged");
        string hitFunction = "GetHit";
        string takingDamage = "TakingDamage";

        entityFx.StartCoroutine(hitFunction);
        StartCoroutine(takingDamage);

    }
    
    public virtual IEnumerator TakingDamage()
    {
        isDamaged = true;
        rb.velocity = new Vector2(damageShaking.x * -facingDir, damageShaking.y);
        yield return new WaitForSeconds(takingDamageDuration);
        isDamaged = false;
    }

    public virtual (Vector2, float) AttackInfos() => 
    (new Vector2(attackCircle.position.x, attackCircle.position.y), attackCirclekRadius);

}
