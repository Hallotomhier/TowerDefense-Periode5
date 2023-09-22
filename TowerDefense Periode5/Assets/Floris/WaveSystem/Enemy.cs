using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    public void Update()
    {
        
    }
    public void Check()
    {
        
        if (spawnManager != null)
        {
            spawnManager.EnemyDefeated();
        }
    }
}
