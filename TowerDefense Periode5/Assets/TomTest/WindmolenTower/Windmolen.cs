using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Windmolen : MonoBehaviour
{
    public Test detectEnemy;

    public float windSpeed = 70f;

    public float slowDownSpeed;

    [Header("GameObjects")]
    public GameObject[] towers;
    public GameObject[] wind;
    

    [Header("Upgrade")]
    public int upgrade;

    public Transform target;
    
    
    


    private void Start()
    {
        upgrade = 1;

    }
    private void Update()
    {
        target = detectEnemy.ChooseTarget;
        

        UpgradeBuilding();
        DraaiWind();
        SlowDown();
        
    }

    private void SlowDown() 
    {
        if (target != null)
        {
            windSpeed = 200f;
            if (target.GetComponent<FollowPath>() != null)
            {
                target.GetComponent<FollowPath>().speed = 2;
            }


            
        }
        else 
        {
            windSpeed = 20;
        }
    
    }




    private void DraaiWind() 
    {
        wind[0].transform.Rotate(new Vector3(0, 0, windSpeed) * Time.deltaTime);
        wind[1].transform.Rotate(new Vector3(0, 0, windSpeed) * Time.deltaTime);
        wind[2].transform.Rotate(new Vector3(0, 0, windSpeed) * Time.deltaTime);
    }
        
    private void UpgradeBuilding() 
    {
        if (upgrade == 1)
        {
            towers[0].SetActive(true);
        }
        else 
        {
            towers[0].SetActive(false);
        }

        if (upgrade == 2)
        {
            towers[1].SetActive(true);
        }
        else
        {
            towers[1].SetActive(false);
        }

        if (upgrade == 3)
        {
            towers[2].SetActive(true);
        }
        else
        {
            towers[2].SetActive(false);
        }

        if (upgrade == 3) 
        {
                
        }   

        if (upgrade <= 0) 
        {
            upgrade = 1;
        }

        if (upgrade > 3) 
        {
            upgrade = 3;
        }
    }



}
