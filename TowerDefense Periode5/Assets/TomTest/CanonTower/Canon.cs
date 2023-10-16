using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private float rotationSpeed = 5f;
    private float returnSpeed = 2f;
    private float triggerDistance = 10f;
    public float maxRotationAngle = 45f;

    public GameObject[] cannons;
    private bool isTargetInRange = false;
    private Quaternion initialLocalRotation;


    private Quaternion initialRotation;
    RaycastHit hit;

    [Header("TargetSettings")]
    public Detect detect;
    public Transform target;

    [Header("DamageSettings")]
    public int[] damage;
    public float[] delay;

    [Header("Level")]
    public GameObject[] towers;
    private float timer;
    public int level = 1;


    public void Start()
    {
        initialLocalRotation = transform.localRotation;
    }
    void Update()
    {

        target = detect.ChooseTarget;

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

        if (level <= 0)
        {
            level = 1;
        }

        if (level >= 3)
        {
            level = 3;
        }
        RotateWithTarget();
        UpgradeTower();
    }

    private void Shoot()
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

    private void UpgradeTower() 
    {
        if (level == 1)
        {
            towers[0].SetActive(true);
        }
        else 
        {
            towers[0].SetActive(false);
        }

        if (level == 2)
        {
            towers[1].SetActive(true);
        }
        else
        {
            towers[1].SetActive(false);
        }

        if (level == 3)
        {
            towers[2].SetActive(true);
        }
        else
        {
            towers[2].SetActive(false);
        }

        
    }



    private void RotateWithTarget()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= triggerDistance)
            {
                
                Vector3 directionToTarget = target.position - transform.position;
                directionToTarget.y = 0; 
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                
                float yRotation = targetRotation.eulerAngles.y;
                yRotation = Mathf.Clamp(yRotation, -maxRotationAngle, maxRotationAngle);
                targetRotation = Quaternion.Euler(0, yRotation, 0);

                
                cannons[level].transform.localRotation = Quaternion.Slerp(cannons[level].transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

                isTargetInRange = true;
            }
            else if (isTargetInRange)
            {
                cannons[level].transform.localRotation = Quaternion.Slerp(cannons[level].transform.localRotation, initialLocalRotation, returnSpeed * Time.deltaTime);
                isTargetInRange = false;
            }
        }
        else
        {

            cannons[level].transform.localRotation = Quaternion.Slerp(cannons[level].transform.localRotation, initialLocalRotation, returnSpeed * Time.deltaTime);
        }
    }
}

