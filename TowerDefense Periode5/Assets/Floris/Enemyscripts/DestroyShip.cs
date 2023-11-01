using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    public List<GameObject> spawnList;
    public Cargo cargo;
    public Transform spawnPointLand;
    public List<GameObject> currentEnemy;
    public GameObject enemyPrefab;
    public Enemy enemyCheck;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyCheck = other.gameObject.GetComponent<Enemy>();
            cargo = other.gameObject.GetComponent<Cargo>();
            currentEnemy.Add(other.gameObject);
            cargo.AddSpawnList();
            StartCoroutine(StandStillAndSpawn());
            

        }
        
    }
   

    private IEnumerator StandStillAndSpawn()
    {
        Debug.Log("Fired");
        while (spawnList.Count > 0)
        {

            
            SpawnEnemies();
            yield return new WaitForSeconds(1.5f);
           

            if(spawnList.Count == 0)
            {
                enemyCheck.Check();
                break;
            }
            foreach (GameObject gameObject in currentEnemy)
            {
                Destroy(gameObject);
            }

        }


    }
    private void SpawnEnemies()
    {
        if (spawnPointLand != null && enemyPrefab != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPointLand.position, Quaternion.identity);
            if (spawnList.Count != 0)
            {
                spawnList.RemoveAt(spawnList.Count - 1);
            }
           
            
            
        }

    }
    
}
