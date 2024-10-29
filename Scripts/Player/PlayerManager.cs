using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(instance.gameObject);
    }

}
