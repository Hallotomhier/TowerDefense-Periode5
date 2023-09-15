using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private string spawnName;
    [SerializeField]
    private List<ScriptableObjecsenem> wave;
    [SerializeField]
    private List<Transform> spawnPositions;
    private int currentWave;
  
    private int instanceIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
