using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGliding : MonoBehaviour
{
    GameObject cam;
    [SerializeField] float glideEffect;
    float distanceToMove;
    float distanceMoved;
    float length;
    float xPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        distanceToMove = cam.transform.position.x * glideEffect;
        distanceMoved = cam.transform.position.x * (1 - glideEffect);

        transform.position = new Vector2(xPos + distanceToMove ,transform.position.y);

        if(distanceMoved > xPos + length)
            xPos = xPos + length;
        else if (distanceMoved < xPos - length)
            xPos = xPos - length;   
    }
}
