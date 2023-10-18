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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SlowDown();
    }
    public void SlowDown()
    {
        if(detection.nearestEnemy != null)
        {
            RotateWindMill();
            enemyPathing = detection.nearestEnemy.GetComponent<FollowPath>();
            unit = detection.nearestEnemy.GetComponent<Unit>();
            if(unit != null)
            {
                unit.speed = 4f;
            }
            else if(unit == null)
            {
                unit.speed = slowdown[level];
            }

            if(enemyPathing != null)
            {
                enemyPathing.speed = 4f;

            }
            else if (enemyPathing == null)
            {
                enemyPathing.speed = slowdown[level];
            }
        }
    }
    public void RotateWindMill()
    {

    }
    public void UpgradeSystem()
    {
        if (towerUpgrade[level])
        {
            towerUpgrade[level].SetActive(true);
            
        }
        else if(towerUpgrade[level])
        {
            towerUpgrade[0].SetActive(false);
            towerUpgrade[level].SetActive(true);
        }
        else if (towerUpgrade[level])
        {
            towerUpgrade[1].SetActive(false);
            towerUpgrade[level].SetActive(true);
        }
    }
}
