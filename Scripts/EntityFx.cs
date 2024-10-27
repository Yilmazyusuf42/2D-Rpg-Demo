using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField] Material hitMat;
    Material orgMat;
    Color orgColor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        orgMat = sr.material;
        orgColor = orgMat.color;
    }

    public IEnumerator GetHit()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = orgMat;
    }

    void RedBlink()
    {
        if(orgMat.color != Color.red)
            orgMat.color = Color.red;
        else 
            orgMat.color = orgColor;
    }

    void CancelRedBlink()
    {
        orgMat.color = orgColor;
        CancelInvoke();
    }


}
