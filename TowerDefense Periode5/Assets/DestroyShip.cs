using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    Unit unit;
    public SpawnManager spawnManager;
    public Enemy enemy;
    public Transform spawnPointLand;
    public GameObject enemyPrefab;
    public int totalEnemiesToSpawn = 5;
    private int spawnedEnemiesLand = 0;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && spawnedEnemiesLand < totalEnemiesToSpawn)
        {
            Enemy enemyComponent = other.GetComponent<Enemy>();

            if (enemyComponent != null)
            {
                enemyComponent.Check();
            }

            StartCoroutine(StandStillAndSpawn());
        }
        while(spawnedEnemiesLand > totalEnemiesToSpawn)
        {
            Testt();

            break;
        }
    }
    private void Testt()
    {
        if (spawnedEnemiesLand >= totalEnemiesToSpawn)
        {
            spawnManager.EnemyDefeated();
            Destroy(unit.gameObject);

            spawnedEnemiesLand = 0;
        }
    }

    private IEnumerator StandStillAndSpawn()
    {
        while (spawnedEnemiesLand < totalEnemiesToSpawn)
        {
            yield return new WaitForSeconds(1.5f);
            SpawnEnemies();
        }

    }

    private void SpawnEnemies()
    {
        if (spawnPointLand != null && enemyPrefab != null && spawnedEnemiesLand < totalEnemiesToSpawn)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPointLand.position, Quaternion.identity);
            spawnedEnemiesLand++;
        }
    }
}
