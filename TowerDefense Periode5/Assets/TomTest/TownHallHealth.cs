using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TownHallHealth : MonoBehaviour
{
    public int townHealth;

    public TMP_Text currentHealth;
    void Start()
    {
        
    }

    
    void Update()
    {
       UpdateHealth();
        if (townHealth <= 0) 
        {
            Debug.Log("GameOver");
        }
    }
    void UpdateHealth()
    {
        currentHealth.text = "Health: " + townHealth.ToString();
    }
}
