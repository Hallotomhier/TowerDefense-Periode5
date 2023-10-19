using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotateevel3 : MonoBehaviour
{
    public float speed;

    public float currentRotation = 0;
    public float maxRotations = 8;

    public float rotationDelay = 1;
    public float timeSinceLastRotation = 0.0f;
    public float targetRotation = 0;

    public bool canRotate = false;

    public void Start()
    {
        StartCoroutine(RotateTurret());
    }
    public IEnumerator RotateTurret()
    {
        while (true)
        {
            if (canRotate)
            {
                quaternion targetRotation = transform.rotation * quaternion.Euler(0, 45, 0);
                float time = 0;
                float totalRotationTime = 45 / speed;
                while(time < totalRotationTime)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time / totalRotationTime);
                    time += Time.deltaTime;
                    yield return null;

                }
                transform.rotation = targetRotation;
                yield return new WaitForSeconds(rotationDelay);

                canRotate = false;
            }
            
            yield return null; 
            
        }
    }


}
