using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Windmolen : MonoBehaviour
{
    [Header("Upgrade")]
    public int upgrade;
    public GameObject[] towers;
   
    public VisualEffect vfxGraph;
    public KeyCode theKey;
    private bool max;


    private void Start()
    {
        upgrade = 1;

    }
    private void Update()
    {
        UpgradeBuilding();
        if (Input.GetKeyUp(theKey)) 
        {
            
            if(!max) 
            {
                vfxGraph.Play();
            }
            upgrade++;
        }
        
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public void PressUpgrade() 
    {
        
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
            max = true;    
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
