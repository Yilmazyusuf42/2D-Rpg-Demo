using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackholeHotKey : MonoBehaviour
{
    SpriteRenderer sr;
    KeyCode myHotkey;
    TextMeshProUGUI text;
    Transform enemyPos;
    BlackHoleSkill_Controller blackHole;

    public void SetupHotkey(KeyCode _newHotKey, Transform _transform, BlackHoleSkill_Controller _blackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        enemyPos = _transform;
        blackHole = _blackHole;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = _newHotKey.ToString();
        myHotkey = _newHotKey;
    }


    private void Update()
    {

        if (Input.GetKeyDown(myHotkey))
        {
            blackHole.AddTargetList(enemyPos);
            sr.color = Color.clear;
            text.color = Color.clear;
            Debug.Log("Bastım suna" + myHotkey);
        }
    }
}
