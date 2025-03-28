using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField] Material hitMat;
    Material orgMat;
    Color orgColor;

    [Header("Ailment Color")]
    [SerializeField] Color[] chilledColor;
    [SerializeField] public Color[] igniteColor;
    [SerializeField] Color[] shockedColor;

    string ignite1 = "FF3434";
    string ignite2 = "F6A96A";

    string shockColor1 = "F3FF00";
    string shockColor2 = "C4CB3B";
    string chillColor1 = "477BEC";
    string chillColor2 = "1F4DB0";
    CharacterStats stats;


    // Start is called before the first frame update
    void Start()
    {
        StartHexColorChanging();

        sr = GetComponent<SpriteRenderer>();
        orgMat = sr.material;
        orgColor = sr.color;
        stats = GetComponentInParent<CharacterStats>();
    }

    public IEnumerator GetHit()
    {
        sr.material = hitMat;
        Color currentColer = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(.2f);
        sr.color = currentColer;
        sr.material = orgMat;
    }

    void RedBlink()
    {
        if (orgMat.color != Color.red)
            orgMat.color = Color.red;
        else
            orgMat.color = orgColor;
    }

    void CancelColorChange()
    {
        orgMat.color = orgColor;
        sr.color = orgColor;
        CancelInvoke();

    }

    void IgniteEffect()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }

    void ShockedEffect()
    {
        if (sr.color != shockedColor[0])
            sr.color = shockedColor[0];
        else
            sr.color = shockedColor[1];
    }

    void ChilledEffect()
    {
        if (sr.color != chilledColor[0])
            sr.color = chilledColor[0];
        else
            sr.color = chilledColor[1];
    }




    public void ChillingFor(float _seconds)
    {
        float repeatingSpeed = .1f;
        InvokeRepeating("ChilledEffect", 0, repeatingSpeed);
        Invoke("CancelColorChange", _seconds);

    }


    public void ShockingFor(float _seconds)
    {
        float repeatingSpeed = .1f;
        InvokeRepeating("ShockedEffect", 0, repeatingSpeed);
        Invoke("CancelColorChange", _seconds);

    }
    public void IgniteFor(float _seconds)
    {
        float repeatingSpeed = .1f;
        InvokeRepeating("IgniteEffect", 0, repeatingSpeed);
        Invoke("CancelColorChange", _seconds);
    }









    Color ChangeHexToColor(string _hex)
    {
        string color = _hex;
        float r = int.Parse(_hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = int.Parse(_hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = int.Parse(_hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        return new Color(r, g, b, 1);
    }


    void StartHexColorChanging()
    {
        igniteColor[0] = ChangeHexToColor(ignite1);
        igniteColor[1] = ChangeHexToColor(ignite2);

        shockedColor[0] = ChangeHexToColor(shockColor1);
        shockedColor[1] = ChangeHexToColor(shockColor2);

        chilledColor[0] = ChangeHexToColor(chillColor1);
        chilledColor[1] = ChangeHexToColor(chillColor2);

    }
}
