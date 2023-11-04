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

                    gameState = GameState.WaveInProgress;
                }
                break;

            case GameState.WaveInProgress:
                if (totalEnemiesDefeated == totalEnemiesSpawned)
                {
                    buildPhaseUi.SetActive(true);
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
                        timerUI = 20;
                        


                    }

                }
                
                break;


            case GameState.BuildPhase:

                isWaveActive = true;
                isBuildPhase = false;
                if (isBuildPhase && timer >= buildPhaseDuration)
                {

                    timer = 0;
                    timerUI = 20;
                    
                    StartNextWave();
                    


                }
                break;

               
        }

       
        UpdateUi();
        timer += Time.deltaTime;
        
    }

    private void StartNextWave()
    {
            currentWave++;
            isBuildPhase = false;
            isWaveActive = true;
            buildPhaseUi.SetActive(false);

            Debug.Log("Starting Wave " + currentWave);
            animator.SetTrigger("BuildPhaseStart");
           
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
   
    public void UpdateUi()
    {
       
        timerText.text = timerUI.ToString("N0");
        // kijk hier nog na vanavond
        if(gameState != GameState.WaveInProgress)
        {
            animator.ResetTrigger("StartBuildPhase");
            
        }
        
        
            
        
       
        
        
    }
}

