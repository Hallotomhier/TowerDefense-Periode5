using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallHealth : MonoBehaviour
{
    public int townHealth;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (townHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
