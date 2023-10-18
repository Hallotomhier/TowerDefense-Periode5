using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public DetectionV2 detect;
    public Transform target;
    public int[] damage;
    public float[] delay;
    public GameObject[] towerLevel;
    public int level;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = detect.nearestEnemy.transform;
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > delay[level])
            {
                Shoot();
                timer -= delay[level];
            }
        }
        else
        {
            timer = 0f;
        }
        RotateToTarget();
    }
    public void RotateToTarget()
    {
        if(target != null)
        {
           if(detect.nearestEnemy != null)
           {
                Vector3 lookAtTarget = detect.nearestEnemy.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget);
                transform.rotation = targetRotation;
           }
        }
    }
    public void Shoot()
    {
        if (target.GetComponent<FollowPath>())
        {
            target.GetComponent<FollowPath>().hp -= damage[level];
        }
        if (target.GetComponent<Unit>())
        {
            target.GetComponent<EnemyHealth>().health -= damage[level];
        }


    }
   
    public void UpgradeSystem()
    {
        if (towerLevel[0])
        {
            towerLevel[level].SetActive(true);

        }
        else if (towerLevel[1])
        {
            towerLevel[0].SetActive(false);
            towerLevel[level].SetActive(true);
        }
        else if (towerLevel[2])
        {
            towerLevel[1].SetActive(false);
            towerLevel[level].SetActive(true);
        }
    }
    
}
