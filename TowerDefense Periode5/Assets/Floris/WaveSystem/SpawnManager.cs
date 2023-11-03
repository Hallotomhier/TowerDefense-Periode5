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
    private float uiTimer = 4f;

    public List<Wave> waves = new List<Wave>();
    private int currentWave = 0;
  
    public bool isBuildPhase = false;
    public bool isWaveActive = false;
    private int totalEnemiesSpawned = 0;
    private int totalEnemiesDefeated = 0;
    
    public GameObject buildPhaseUi;
    public Image image;
    public AnimationClip animationClips;
    public Animator animator;

    private enum GameState { WaitingForStart, WaveInProgress, BuildPhase }
    private GameState gameState = GameState.WaitingForStart;

    private void Start()
    {
       
        timer += Time.deltaTime;
        gameState = GameState.WaveInProgress;
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
              
                if (totalEnemiesDefeated == totalEnemiesSpawned)
                {
                   
                    isBuildPhase = true;

                    buildPhaseUi.SetActive(true);
                    timer += Time.deltaTime;
                    timerUI -= Time.deltaTime;

                    if (timer >= buildPhaseDuration)
                    {
                        totalEnemiesDefeated = 0;
                        totalEnemiesSpawned = 0;
                        StartBuildPhase();
                       
                    }
                   
                }
                break;

            case GameState.BuildPhase:

               
                if (isBuildPhase && timer >= buildPhaseDuration)
                {
                    
                    timer = 0;
                   
                    StartNextWave();
                    
                    
                }
                break;
        }

       
        UpdateUi();
       
    }

    private void StartNextWave()
    {
        if (totalEnemiesDefeated >= totalEnemiesSpawned)
        {
            currentWave++;
            isBuildPhase = false;
            isWaveActive = true;
            

            Debug.Log("Starting Wave " + currentWave);
           
            if(currentWave <= waves.Count)
            {
                StartCoroutine(SpawnWaveEnemies(currentWave));

            }

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
                float delay = wave.enemyDelays[i];
                yield return new WaitForSeconds(delay);
                
                


            }
        }
        gameState = GameState.WaveInProgress;

    }

    private void StartBuildPhase()
    {
        gameState = GameState.BuildPhase;
        timerUI = 20;
    }

   public void EnemyDefeated()
   {
        totalEnemiesDefeated++;
        Debug.Log("Total Enemies Defeated: " + totalEnemiesDefeated);
        
   }
   
    public void UpdateUi()
    {
       
        timerText.text = timerUI.ToString("N0");
        // kijk hier nog na vanavond
        if (timer >= uiTimer && animHasPlayed == false)
        {
            animator.Play("UIAnim", 1);
            animHasPlayed = true;
            
           
        }
        if(timer <= uiTimer && animHasPlayed == true)
        {
            animHasPlayed = false;
        }
    }
}

