using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BlackHoleSkill : MonoBehaviour
{
    public float maxScale;
    [Range(0f, 1f)] public float scaleSpeed;

    public bool canScale;

    // Update is called once per frame
    void Update()
    {
        if (canScale)
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * maxScale, scaleSpeed * Time.deltaTime);

    }
}
