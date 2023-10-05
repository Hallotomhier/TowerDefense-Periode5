using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Windmolen : MonoBehaviour
{
    

    public float windSpeed = 70f;

    public float slowDownSpeed;

    [Header("GameObjects")]
    public GameObject[] towers;
    public GameObject[] wind;
    

    [Header("Upgrade")]
    public int upgrade;

    public Transform target;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            windSpeed = 200f;
            if (other.GetComponent<FollowPath>()) 
            {
                other.GetComponent<FollowPath>().speed = slowDownSpeed;
            }

            if (other.GetComponent<Unit>())
            {
                other.GetComponent<Unit>().speed = slowDownSpeed;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<FollowPath>())
            {
                other.GetComponent<FollowPath>().speed = 6f;
            }

            if (other.GetComponent<Unit>())
            {
                other.GetComponent<Unit>().speed = 6f;
            }
        }
    }



    private void Start()
    {
        upgrade = 1;

    }
    private void Update()
    {
        UpgradeBuilding();
        DraaiWind();
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
            slowDownSpeed = 4;
        }
        else 
        {
            towers[0].SetActive(false);
        }

        if (upgrade == 2)
        {
            towers[1].SetActive(true);
            slowDownSpeed = 3;
        }
        else
        {
            towers[1].SetActive(false);
        }

        if (upgrade == 3)
        {
            towers[2].SetActive(true);
            slowDownSpeed = 2;
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
