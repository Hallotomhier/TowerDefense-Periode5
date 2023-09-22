using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyTypes;
    public int[] enemyCounts;
}

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float startTime = 50f;
    public float timeBetweenWaves = 10f;
    public float buildPhaseDuration;

    public List<Wave> waves = new List<Wave>();
    private int currentWave = 0;
    private bool isWaveActive = false;
    private bool isBuildPhase = false;

    private int totalEnemiesSpawned = 0;
    private int totalEnemiesDefeated = 0;

    private enum GameState { WaitingForStart, WaveInProgress, BuildPhase }
    private GameState gameState = GameState.WaitingForStart;

    private void Start()
    {
        
        gameState = GameState.WaitingForStart;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingForStart:
                if (Time.time >= startTime)
                {
                    StartNextWave();
                }
                break;

            case GameState.WaveInProgress:
              
                if (totalEnemiesDefeated >= totalEnemiesSpawned)
                {
                   
                    StartBuildPhase();
                }
                break;

            case GameState.BuildPhase:
               
                if (isBuildPhase && Time.time >= buildPhaseDuration)
                {
                    isBuildPhase = false;
                    Debug.Log("NextWave");
                    StartNextWave();
                }
                break;
        }
        Debug.Log("Total Enemies Spawned: " + totalEnemiesSpawned);

        Debug.Log("Game State: " + gameState);
        Debug.Log("isWaveActive: " + isWaveActive);
        Debug.Log("isBuildPhase: " + isBuildPhase);
    }

    private void StartNextWave()
    {
        if (totalEnemiesDefeated == totalEnemiesSpawned)
        {
            currentWave++;
            isWaveActive = true;
            

            Debug.Log("Starting Wave " + currentWave);

            StartCoroutine(SpawnWaveEnemies(currentWave));

            gameState = GameState.WaveInProgress;
        }
    }

    private IEnumerator SpawnWaveEnemies(int currentWave)
    {
        Wave wave = waves[currentWave -1];

        for (int i = 0; i < wave.enemyTypes.Length; i++)
        {
            for (int j = 0; j < wave.enemyCounts[i]; j++)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(wave.enemyTypes[i], spawnPoints[spawnPointIndex].position, Quaternion.identity);
                totalEnemiesSpawned++;
                gameState = GameState.WaveInProgress;
                yield return new WaitForSeconds(1f);
                
            }
        }
    }

    private void StartBuildPhase()
    {
        isBuildPhase = true;
        Debug.Log("Entering Build Phase...");
        totalEnemiesDefeated = 0;
        totalEnemiesSpawned = 0;
        
        gameState = GameState.BuildPhase;
    }

   public void EnemyDefeated()
   {
        Debug.Log("Total Enemies Defeated: " + totalEnemiesDefeated);
        totalEnemiesDefeated++;
   }
    
}

