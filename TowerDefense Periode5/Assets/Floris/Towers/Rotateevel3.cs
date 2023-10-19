using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotateevel3 : MonoBehaviour
{
    public float speed;
    public float degree = 45;
   
    public void Update()
    {
        RotateLevel3();
    }

    public void RotateLevel3()
    {


        StartCoroutine(RotateMe(Vector3.right * 45, 1.5f));

    }
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

}
