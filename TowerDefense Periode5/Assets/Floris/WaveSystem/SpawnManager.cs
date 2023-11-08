using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyTypes;
    public int[] enemyCounts;
    public float[] enemyDelays;
}

public class SpawnManager : MonoBehaviour
{
    public float clock;
    public TMP_Text timerText;
    public bool animHasPlayed = false;
    public Transform[] spawnPoints;
    public float startTime = 50f;
    public float timeBetweenWaves = 10f;
    public float buildPhaseDuration;
    public float timerUI= 20;
    public float timer;
    public GameObject victoryScreen;
   

    public List<Wave> waves = new List<Wave>();
    public int currentWave = 0;
  
    public bool isBuildPhase = false;
    public bool isWaveActive = false;
    public int totalEnemiesSpawned = 0;
    public int totalEnemiesDefeated = 0;
    
    public GameObject buildPhaseUi;
    public Image image;
    public AnimationClip animationClips;
    public Animator animator;

    public Button buttonNextWave;
    public Button buttonSpawnRocks;
    public Button buttonSpawnRaft;

    public enum GameState { WaitingForStart, WaveInProgress, BuildPhase }
    public GameState gameState = GameState.WaitingForStart;

    private void Start()
    {
       
        
        gameState = GameState.WaitingForStart;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingForStart:
                if (timer >= startTime)
                {
                    Debug.Log(gameState);
                    animator.SetBool("PlayBuildphase", true);
                    gameState = GameState.WaveInProgress;
                }
                break;

            case GameState.WaveInProgress:
                if (totalEnemiesDefeated == totalEnemiesSpawned)
                {
                    buildPhaseUi.SetActive(true);
                    buttonNextWave.interactable = true;
                    buttonSpawnRaft.interactable = true;
                    buttonSpawnRocks.interactable = true;
                    Debug.Log(gameState);
                    isBuildPhase = true;
                    isWaveActive = false;

                    
                   
                   
                    timerUI -= Time.deltaTime;


                    if (timerUI <= 1)
                    {
                        totalEnemiesDefeated = 0;
                        totalEnemiesSpawned = 0;
                        
                        timer = 0;

                        StartNextWave();
                        timerUI = 40;



                    }

                }
                
                break;


            case GameState.BuildPhase:

                isWaveActive = true;
                isBuildPhase = false;
                if (isWaveActive && timer >= buildPhaseDuration)
                {

                    timer = 0;
                    timerUI = 40;
                    animator.SetBool("PlayTimer", true);

                   

                    StartNextWave();
                    


                }
                break;

               
        }

       if(totalEnemiesSpawned == totalEnemiesDefeated)
       {
            if (currentWave == waves.Count)
            {
                Victory();
                Time.timeScale = 0;
            }
        }
       
        UpdateUi();
        timer += Time.deltaTime;
        
    }

    public void StartNextWave()
    {

        buttonNextWave.interactable = false;
        buttonSpawnRaft.interactable = false;
        buttonSpawnRocks.interactable = false;

        currentWave++;
            isBuildPhase = false;
            isWaveActive = true;
            buildPhaseUi.SetActive(false);

            Debug.Log("Starting Wave " + currentWave);
            
           
            if(currentWave < waves.Count)
            {
                StartCoroutine(SpawnWaveEnemies(currentWave));

            }

            gameState = GameState.WaveInProgress;
            
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
                float delay = wave.enemyDelays[i];
                yield return new WaitForSeconds(delay);
                
                


            }
        }
        gameState = GameState.WaveInProgress;

    }

   

    public void EnemyDefeated()
    {
        totalEnemiesDefeated++;
        Debug.Log("Total Enemies Defeated: " + totalEnemiesDefeated);
        
    }
   
    public void Victory()
    {
        victoryScreen.SetActive(true);
    }
    public void UpdateUi()
    {
       
        timerText.text = timerUI.ToString("N0");
       
        if(gameState != GameState.WaveInProgress)
        {
            animator.SetBool("PlayBuildPhase",false);
            
        }
        
        
            
        
       
        
        
    }
}

