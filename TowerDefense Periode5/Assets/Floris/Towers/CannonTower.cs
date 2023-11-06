using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    public Rotateevel3 rotations;
    public DetectionV2 detect;
    public Transform target;
    public RotateTarget rotateTargetLevel0;
    public RotateTarget rotateTargetLevel1;
    public int[] damage;
    public float[] delay;
    public GameObject[] towerLevel;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    
    public int level;
    public float timer;
    public bool canRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 2)
        {
            if (detect.nearestEnemy != null)
            {
                target = detect.nearestEnemy.transform;
                rotations.canRotate = true;
            }
        }
        
        if( level == 0)
        {
            if(detect.nearestEnemy != null)
            {
                target = detect.nearestEnemy.transform;
                rotateTargetLevel0.RotateToTarget();
            }
        }
        if(level == 1)
        {
            if(detect.nearestEnemy != null)
            {
                target = detect.nearestEnemy.transform;
                rotateTargetLevel1.RotateToTarget();
            }
        }
        
       
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > delay[level])
            {
                Shoot();
                timer = 0;
            }
        }
        else
        {
            timer = 0f;
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
            Debug.Log("Shot");

        }


    }
   
    public void UpgradeSystem()
    {
        if (towerLevel[level] == towerLevel[0])
        {
            towerLevel[level].SetActive(true);
        }
        
        if (towerLevel[level] == towerLevel[1])
        {
            towerLevel[level].SetActive(true);
            level1.SetActive(false);
            
        }
        
        if (towerLevel[level] == towerLevel[2])
        {
            towerLevel[level].SetActive(true);
            level2.SetActive(false);
            
        }
    }
    
}
