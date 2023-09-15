using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private string spawnName;
    [SerializeField]
    private List<ScriptableObjecsenem> wave;
    [SerializeField]
    private Vector3[] spawnPositions;
    private int currentWave = 0;
    private int instanceIndex = 1;
    [SerializeField]
    private int maxSpawns = 5;
    [SerializeField]
    private int spawned;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaveRoutine());
    }
    IEnumerator StartWaveRoutine()
    {
        while(currentWave < wave[currentWave].waveComposition.Count)
        {
            StartCoroutine(SpawnEnemiesRoutine());
            yield return new WaitForSeconds(5f);
            GameObject[] activeEnemys = GameObject.FindGameObjectsWithTag("Enemies");

            currentWave++;
        }
    }
   
    IEnumerator SpawnEnemiesRoutine()
    {
        
        if ( spawned <= maxSpawns)
        {
            int currentSpawnPointIndex = 0;
            for (int i = 0; i < wave[currentWave].waveComposition.Count; i++)
            {
                var obj = wave[currentWave].waveComposition[i];
                GameObject currentEnemy = Instantiate(obj, spawnPositions[currentSpawnPointIndex], Quaternion.identity);
                new WaitForSeconds(2f);
                currentEnemy.name = spawnName + instanceIndex;
                currentSpawnPointIndex++;
                instanceIndex++;
                spawned++;
               
                
            }
            
        }
        yield return new WaitForSeconds(2f);




    }

    public void StartWave()
    {

    }
    public void FinishedWave()
    {

    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
}
