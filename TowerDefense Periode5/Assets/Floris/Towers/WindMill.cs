using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    public DetectionV2 detection;
    public Unit unit;
    public FollowPath enemyPathing;
    public int[] slowdown;
    public GameObject[] towerUpgrade;
    public int level;
    public Rotattiewindmill rotateWindmill0;
    public Rotattiewindmill rotateWindmill1;
    public Rotattiewindmill rotateWindmill2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(level == 0)
       {
            if(detection.nearestEnemy != null)
            {
                rotateWindmill0.canRotate = true;
                SlowDown();
                
            }
            else if(detection.nearestEnemy == null)
            {
                rotateWindmill0.canRotate = false;
            }
        }

        if (level == 1)
        {
            if (detection.nearestEnemy != null)
            {
                rotateWindmill1.canRotate = true;
                SlowDown();
               
            }
            else
            {
                rotateWindmill1.canRotate = false;
            }
        }

        if (level == 2)
        {
            if (detection.nearestEnemy != null)
            {
                rotateWindmill2.canRotate = true;
                SlowDown();
                
            }
            else
            {
                rotateWindmill2.canRotate = false;
            }
        }
    }
    public void SlowDown()
    {
        
        if(detection.nearestEnemy != null)
        {
           
            enemyPathing = detection.nearestEnemy.GetComponent<FollowPath>();
            unit = detection.nearestEnemy.GetComponent<Unit>();
            if(unit != null)
            {
                unit.speed = slowdown[level];
               
            }
            else if(unit == null)
            {
               
                unit.speed = 4f;
            }

            if(enemyPathing != null)
            {
                enemyPathing.speed = slowdown[level];
               

            }
            else if (enemyPathing == null)
            {
               
                enemyPathing.speed = 4f;
            }
        }
    }
   
    public void UpgradeSystem()
    {
        if (towerUpgrade[level] == towerUpgrade[0])
        {
            towerUpgrade[level].SetActive(true);
            
        }
        else if(towerUpgrade[level] == towerUpgrade[1])
        {
            towerUpgrade[0].SetActive(false);
            towerUpgrade[level].SetActive(true);
        }
        else if (towerUpgrade[level] == towerUpgrade[2])
        {
            towerUpgrade[1].SetActive(false);
            towerUpgrade[level].SetActive(true);
        }
    }
}
