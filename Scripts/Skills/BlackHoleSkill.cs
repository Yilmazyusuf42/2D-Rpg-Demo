using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BlackHoleSkill : MonoBehaviour
{
    [SerializeField] private GameObject blackHoleKey;
    [SerializeField] private List<KeyCode> hotKeys = new List<KeyCode>();
    List<Transform> targets = new List<Transform>();
    public float maxScale;
    [Range(0f, 1f)] public float scaleSpeed;

    public bool canScale;

    // Update is called once per frame
    void Update()
    {
        if (canScale)
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * maxScale, scaleSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().BeingFreeze(true);
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
        GameObject newBlackholeKey = Instantiate(blackHoleKey, other.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        BlackholeHotKey hotKey = newBlackholeKey.GetComponent<BlackholeHotKey>();
        KeyCode theKey = hotKeys[UnityEngine.Random.Range(0, hotKeys.Count)];
        hotKey.SetupHotkey(theKey, other.transform, this);
        hotKeys.Remove(theKey);
    }

    public void AddTargetList(Transform _transform) => targets.Add(_transform);
}
