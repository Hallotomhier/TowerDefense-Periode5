using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceVogel : MonoBehaviour
{
    public float timer;
    public SoundManager SoundManager;
    [Header("Delay")]
    public float[] delay;

    [Header("Level")]
    public int level;

    [Header("Vogel")]
    public GameObject littleBoy;
    public GameObject[] towerLevel;
    

    [Header("Target")]
    public Transform target;
    public Transform above;

    [Header("scripts")]
    public DetectionV2 detectEnemy;

    void Update()
    {
        if(target == null)
        {
            target = detectEnemy.nearestEnemy.transform;
        }
       
        Delay();

    }


    private void Delay() 
    {
        
        if (target != null) 
        {
            timer += Time.deltaTime;
            if (timer >= delay[level]) 
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
        SoundManager.PlaySfx("Seagull");
        VogelBoem birdScript = newBird.GetComponent<VogelBoem>();
        birdScript.target = detectEnemy.nearestEnemy;

    }

   

    public void UpgradeSystemBird()
    {
        if (towerLevel[level] == towerLevel[0])
        {
            towerLevel[level].SetActive(true);

        }
        else if (towerLevel[level] == towerLevel[1])
        {
            towerLevel[0].SetActive(false);
            towerLevel[level].SetActive(true);
        }
        else if (towerLevel[level] == towerLevel[2])
        {
            towerLevel[1].SetActive(false);
            towerLevel[level].SetActive(true);
        }
    }
}
