using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    public List<GameObject> spawnList;
    public Cargo cargo;
    public Transform spawnPointLand;
    public GameObject currentEnemy;
    public GameObject enemyPrefab;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Cargo>())
        {
           cargo = other.gameObject.GetComponent<Cargo>();
            currentEnemy = other.gameObject;
            AddSpawnList();
           
        }
       
    }
    public void AddSpawnList()
    {
       cargo.cargoList.Capacity = spawnList.Capacity;
    }

    private IEnumerator StandStillAndSpawn()
    {
        while (spawnList.Count > 1)
        {
            yield return new WaitForSeconds(1.5f);
            SpawnEnemies();

            Destroy(currentEnemy);

        }


    }
    private void SpawnEnemies()
    {
        if (spawnPointLand != null && enemyPrefab != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPointLand.position, Quaternion.identity);
            spawnList.RemoveAt(spawnList.Count - 1);
        }

    }
    /*
    Unit unit;
    public SpawnManager spawnManager;
    public Enemy enemy;
    
   
    public int totalEnemiesToSpawn = 5;
    public int spawnedEnemiesLand = 0;
    public GameObject testtt;

    private void Update()
    {
        if(testtt == null)
        {
            ResetCounter();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && spawnedEnemiesLand < totalEnemiesToSpawn)
        {
           

            StartCoroutine(StandStillAndSpawn());
        }

        testtt = other.gameObject;

    }

    

   
    private void ResetCounter()
    {
        Debug.Log("Fired");
        spawnedEnemiesLand = 0;
      
    }
    */
}
