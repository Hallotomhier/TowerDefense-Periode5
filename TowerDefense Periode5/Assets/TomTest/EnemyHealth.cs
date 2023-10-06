using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public Unit unit;
    SpawnManager spawnManager;
    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    public void Update()
    {
       
        if(health <= 1)
        {
            unit.MarkAsDestroyed();
        }
    }

}
