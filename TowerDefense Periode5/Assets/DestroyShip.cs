using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
         
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

            spawnManager.activeEnemys.Remove(other.gameObject);
            
            Destroy(other.gameObject);
        }
    }
}
