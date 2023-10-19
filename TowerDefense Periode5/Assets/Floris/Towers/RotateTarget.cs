using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateTarget : MonoBehaviour
{
    public CannonTower tower;
    public DetectionV2 detect;

    private void Update()
    {
        RotateToTarget();
    }
    public void RotateToTarget()
    {
        if (tower.target != null)
        {
            if (detect.nearestEnemy != null)
            {
                Vector3 lookAtTarget = detect.nearestEnemy.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget);
                transform.rotation = targetRotation;
            }
        }
    }

}
