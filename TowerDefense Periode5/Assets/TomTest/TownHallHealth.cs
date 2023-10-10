using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TownHallHealth : MonoBehaviour
{
    public int townHealth;
    public GameObject gameOverPanel;
    public GameObject panelUI;
    public GameObject panelBuilding;
    public TMP_Text currentHealth;
    void Start()
    {
        
    }

    
    void Update()
    {
       UpdateHealth();
        if (townHealth <= 0) 
        {
            panelBuilding.SetActive(false);
            panelUI.SetActive(false);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }
    void UpdateHealth()
    {
        currentHealth.text = "Health: " + townHealth.ToString();
    }
}
