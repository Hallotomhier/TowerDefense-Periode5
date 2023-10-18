using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    public DetectionV2 detection;
    public Unit unit;
    public FollowPath enemyPathing;
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
                unit.speed = 2f;
            }

            if(enemyPathing != null)
            {
                enemyPathing.speed = 4f;

            }
            else if (enemyPathing == null)
            {
                enemyPathing.speed = 2f;
            }
        }
    }
    public void RotateWindMill()
    {

    }
}
