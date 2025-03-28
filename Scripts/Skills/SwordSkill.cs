using UnityEngine;

public enum SwordType
{
    Normal,
    Bouncing,
    Pierce,
    Spin
}

public class SwordSkill : Skills
{

    [SerializeField] private SwordType swordType = SwordType.Normal;

    [Header("Bounce İnfo")]
    [SerializeField] private int amountOfBounce;
    [SerializeField] private float bouncingSpeed;
    [SerializeField] private float bounceGravity;


    [Header("Pierce İnfo")]
    [SerializeField] private int amountPierce;
    [SerializeField] private float pierceGravity;
    [SerializeField] private float pierceingSpeed;



    [Header("Spin İnfo")]
    [SerializeField] private float maxTravelDistance;
    [SerializeField] private float hitCoolDown = .35f;
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinGravity;

    [Space(20)]

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float maxDistanceForSword;
    [SerializeField] private float enemyFreezeDuration;

    Vector2 finalDestination;

    #region Dotsİnfo
    [Header("Aim Dots ")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetwenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dotsList;
    #endregion


    private void Awake()
    {
        GenerateDots();

        SetupGravity();

    }

    private void SetupGravity()
    {
        if (swordType == SwordType.Bouncing)
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDestination = new Vector2(AimingtheSword().normalized.x * launchForce.x, AimingtheSword().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
            for (int i = 0; i < numberOfDots; i++)
                dotsList[i].transform.position = DotsPosition(i * spaceBetwenDots);


    }


    public void CreateSword()
    {
        GameObject theSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController swordSkillController = theSword.GetComponent<SwordSkillController>();

        if (swordType == SwordType.Bouncing)
        {
            swordSkillController.SetupBouncing(amountOfBounce, true, bouncingSpeed);
        }
        else if (swordType == SwordType.Pierce)
        {
            swordSkillController.SetupPierce(amountPierce, true, pierceingSpeed);
        }
        else if (swordType == SwordType.Spin)
        {
            swordSkillController.SetupSpining(true, spinDuration, maxTravelDistance, hitCoolDown);
        }

        swordSkillController.SetupSword(finalDestination, swordGravity, player, enemyFreezeDuration, maxDistanceForSword);
        player.AssingtheSword(theSword);
        DotsActive(false);

    }




    #region Dots

    public Vector2 AimingtheSword()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - playerPosition;

        return direction;
    }



    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < numberOfDots; i++)
            dotsList[i].SetActive(isActive);
    }

    private void GenerateDots()
    {
        dotsList = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dotsList[i] = Instantiate(dotsPrefab, dotsParent);
            dotsList[i].SetActive(false);
        }
        Debug.Log("Noktalar üretildi");
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimingtheSword().normalized.x * launchForce.x,
            AimingtheSword().normalized.y * launchForce.y) * t + .5f * (t * t) * (Physics2D.gravity * swordGravity);

        return position;
    }
    #endregion
}
