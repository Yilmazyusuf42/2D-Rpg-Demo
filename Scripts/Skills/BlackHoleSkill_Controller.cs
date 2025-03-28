using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BlackHoleSkill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject blackHoleKey;
    [SerializeField] private List<KeyCode> hotKeys = new List<KeyCode>();
    List<Transform> targets = new List<Transform>();
    List<GameObject> hotkeyObjects = new List<GameObject>();
    public float maxScale;
    [Range(0f, 1f)] public float scaleSpeed;
    public bool canScale = true;
    bool canShrink = false;
    float blackholeTimer;

    bool canPlayerDissappear = true;
    bool cloneAttackReleased = false;
    int amountofAttack = 3;
    float cloneCoolDown = .3f;
    float coolDownTimer;
    float ofsetRange = 1;


    void Start()
    {
        if (SkillManager.instance.cloneAbility.crystalInsteadofClone)
            canPlayerDissappear = false;
    }


    // Update is called once per frame
    void Update()
    {

        coolDownTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;


        if (blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;

            if (targets.Count > 0)
                cloneAttackReleased = true;
            else
                FinishBlackholeAbility();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (targets.Count > 0)
            {
                cloneAttackReleased = true;
                DestroyHotkeys();
            }
        }

        if (canScale && !canShrink)
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * maxScale, scaleSpeed * Time.deltaTime);

        // Close the hole and released the enemies#
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-5, -5), Time.deltaTime * scaleSpeed * 3);
            if (transform.localScale.x < 1)
                Destroy(gameObject);
        }

        // Create randome clones at position from the targets list.
        if (coolDownTimer < 0 && cloneAttackReleased && amountofAttack > 0)
        {
            ReleasedAttackingClone();

            if (amountofAttack <= 0)
            {
                Invoke("FinishBlackholeAbility", 1f);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other) => other.GetComponent<Enemy>()?.BeingFreeze(false);

    private void FinishBlackholeAbility()
    {
        DestroyHotkeys();
        cloneAttackReleased = false;
        canShrink = true;
        PlayerManager.instance.player.ExitBlackholeAbility();
    }

    private void ReleasedAttackingClone()
    {
        coolDownTimer = cloneCoolDown;
        amountofAttack--;
        Debug.Log($"Relaased attack {amountofAttack}");
        if (canPlayerDissappear)
        {
            canPlayerDissappear = false;
            PlayerManager.instance.player.Dissappearing(true);
        }

        ofsetRange = (Random.Range(0, 100) > 50) ? ofsetRange : -ofsetRange;

        SkillManager.instance.cloneAbility.CreateClone(targets[Random.Range(0, targets.Count)], new Vector3(ofsetRange, 0));


    }

    public void BlackHoleSetup(float _maxScale, float _scaleSpeed, int _amountofAttack, float _cloneCoolDown, float _duration)
    {
        maxScale = _maxScale;
        scaleSpeed = _scaleSpeed;
        amountofAttack = _amountofAttack;
        cloneCoolDown = _cloneCoolDown;
        blackholeTimer = _duration;
    }


    private void DestroyHotkeys()
    {
        if (hotkeyObjects.Count <= 0)
            return;

        for (int i = 0; i < hotkeyObjects.Count; i++)
        {
            Destroy(hotkeyObjects[i]);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().BeingFreeze(true);
            if (!cloneAttackReleased)
                CreateHotkey(other);
        }

    }

    private void CreateHotkey(Collider2D other)
    {
        if (hotKeys.Count <= 0)
        {
            Debug.Log("Paşam yeterli tuş takımına sahip değilsin. Lütfen BlackHole Yetenek ağacına Tuş tekle");
            return;
        }

        if (canShrink)
            return;

        //instantiate on top of the enemy a Blackhole Gameobject
        GameObject newBlackholeKey = Instantiate(blackHoleKey, other.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        hotkeyObjects.Add(newBlackholeKey);

        //place a letter inside the Textmesh compenent of the instantiated object
        BlackholeHotKey hotKey = newBlackholeKey.GetComponent<BlackholeHotKey>();
        KeyCode theKey = hotKeys[Random.Range(0, hotKeys.Count)];
        hotKey.SetupHotkey(theKey, other.transform, this);
        hotKeys.Remove(theKey);
    }

    public void AddTargetList(Transform _transform) => targets.Add(_transform);



}
