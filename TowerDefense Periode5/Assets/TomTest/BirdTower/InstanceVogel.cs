using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceVogel : MonoBehaviour
{
    private float timer;

    [Header("Delay")]
    public float[] delay;

    [Header("Level")]
    public int level;

    [Header("Vogel")]
    public GameObject littleBoy;
    

    [Header("Target")]
    public Transform target;
    public Transform above;

    [Header("scripts")]
    public DetectionV2 detectEnemy;

    void Update()
    {
        target = detectEnemy.nearestEnemy.transform;
        Delay();

    }


    private void Delay() 
    {
        
        if (target != null) 
        {
            timer += Time.deltaTime;
            if (timer > delay[level]) 
            {
                VogelSpawn();
                timer -= delay[level];
            }
        }
    }
    private void VogelSpawn() 
    {
        Vector3 aboveTower = above.position;
        GameObject newBird = Instantiate(littleBoy, aboveTower, Quaternion.identity);
        VogelBoem birdScript = newBird.GetComponent<VogelBoem>();
        birdScript.target = detectEnemy.nearestEnemy;

    }

    
}
