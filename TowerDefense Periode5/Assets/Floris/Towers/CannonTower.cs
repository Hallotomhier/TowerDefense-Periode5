using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public SoundManager soundManager;
    public GameObject bulletPrefab;
    public GameObject[] shootpoint;
    public float bulletSpeed;
    public GameObject bulletActive;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public FollowPath followPath;
    public int level;
    public float timer;
    public bool canRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        bulletPrefab = GameObject.Find("CannonBall");
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
            followPath = target.GetComponent<FollowPath>();
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
        if(target != null && bulletActive == null)
        {
            

            bulletActive = Instantiate(bulletPrefab, shootpoint[level].transform.position, quaternion.identity);
            soundManager.PlaySfx("Cannon");

            StartCoroutine(MoveToTarget());

        }
        


    }
    public IEnumerator MoveToTarget()
    {
        while (bulletActive !=null && target != null)
        {
            if(target == null || target.gameObject.activeSelf == false)
            {
                Destroy(bulletActive);
                bulletActive = null;
                yield break;
            }
            float distanceToTarget = Vector3.Distance(bulletActive.transform.position, target.position);
            while (distanceToTarget <= 0.1f)
            {
                Destroy(bulletActive);
                bulletActive = null;
                if (target.GetComponent<FollowPath>())
                {
                    
                    target.GetComponent<FollowPath>().hp -= damage[level];
                    if(followPath.hp <= followPath.oldHP)
                    {
                        soundManager.PlaySfx("DieEnemy");
                        followPath.oldHP = followPath.hp;
                    }

                }
                if (target.GetComponent<Unit>())
                {
                    target.GetComponent<EnemyHealth>().health -= damage[level];
                    Debug.Log("Shot");
                }
                yield break;
            }

            bulletActive.transform.position = Vector3.MoveTowards(bulletActive.transform.position, target.position, bulletSpeed * Time.deltaTime);
           yield return null;
           
           
        }
       
    }
   
    public void SpawnBullet()
    {
        if(bulletPrefab != null && target != null)
        {
            float speedBullet = bulletSpeed * Time.deltaTime;
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
