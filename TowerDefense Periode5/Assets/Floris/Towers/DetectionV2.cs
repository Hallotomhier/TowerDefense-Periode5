using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionV2 : MonoBehaviour
{
    public Transform target;
    public float range = 20f;
    public GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            return;
        }
    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance =Mathf.Infinity;
        nearestEnemy = null;
        foreach(var enemy in enemies)
        {
           float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            if(distanceEnemy < shortestDistance)
            {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range);
        Gizmos.color = Color.red;
        
    }



}
