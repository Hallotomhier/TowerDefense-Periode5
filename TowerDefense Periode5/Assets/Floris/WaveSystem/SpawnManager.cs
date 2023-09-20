using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Unit unit;
    public GameObject targetGoTo;
    
    [SerializeField]
    private string spawnName;
    
    
    public List<ScriptableObjecsenem> wave;
    public List<GameObject> activeEnemys = new List<GameObject>();
    
    public Vector3[] spawnPositions;
    private int currentWave = 0;
    public int instanceIndex = 1;
   
    public int maxSpawns;
    public bool buildPhase = false;
    private float buildPhaseDuration = 10f;
    public int spawned;
    public bool finishedWave;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(StartWaveRoutine());
    }
    IEnumerator StartWaveRoutine()
    {
        while(currentWave < wave.Count)
        {
            if (currentWave >= wave.Count)
            {
                break;
            }
            if (currentWave < wave[currentWave].waveComposition.Count)
            {

                StartCoroutine(SpawnEnemiesRoutine());
                yield return new WaitWhile(AllEnemysDefeates);
                Debug.Log("Start build phase");
                buildPhase = true;
                yield return new WaitForSeconds(buildPhaseDuration);
                buildPhase = false;
                Debug.Log("Next Wave");
                currentWave++;

            }
            else
            {
                Debug.LogError("Invalid wave composition for currentWave: " + currentWave);
                
            }
        }
        
        
    }
   
    IEnumerator SpawnEnemiesRoutine()
    {
        int currentSpawnPointIndex = 0;
        while ( spawned <= maxSpawns)
        {
            if (currentSpawnPointIndex >= spawnPositions.Length)
            {
                break;
            }
            if (currentSpawnPointIndex < wave[currentWave].waveComposition.Count)
            {
                var obj = wave[currentWave].waveComposition[currentSpawnPointIndex];

                GameObject currentEnemy = Instantiate(obj, spawnPositions[currentSpawnPointIndex], Quaternion.identity);

                currentEnemy.name = spawnName + instanceIndex;
                currentSpawnPointIndex++;
                instanceIndex++;
                spawned++;
                activeEnemys.Add(currentEnemy);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(4f);
        }


      



    }
    bool AllEnemysDefeates()
    {
        Debug.Log("Bool");
        
        
        return activeEnemys.Count == 0;

    }
}
