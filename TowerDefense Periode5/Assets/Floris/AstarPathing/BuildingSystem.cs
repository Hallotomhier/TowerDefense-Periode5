using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    [Header("Script")]
    public Grid grid;
    public PathFinding pathFinding;
    public SpawnManager spawnManager;
    public BuildingSystem building;
    [Header("Prefab")]
    public GameObject buildingPrefab;

    public bool isCheckingPath = false;
    public GameObject startPosition;
    public GameObject targetPosition;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    void Update()
    {
        
        BuilderRocks();
       
        
    }
    public void BuilderRocks()
    {
        if (spawnManager.isBuildPhase)
        {
            if (Keyboard.current.bKey.wasPressedThisFrame && !isCheckingPath)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    if (node != null && node.walkable)
                    {
                        Vector3 buildingPosition = node.worldPosition;

                        // Set a flag to prevent multiple path checks at once
                        isCheckingPath = true;

                        // Check path availability asynchronously
                        pathFinding.FindPathAsync(startPosition.transform.position, targetPosition.transform.position, (path) => OnPathAvailableCheck(path));
                    }
                }
            }
        }
    }
    private void OnPathAvailableCheck(List<Node> path)
    {
        // Reset the flag after path check is done
        isCheckingPath = false;

        if (path != null && path.Count > 0)
        {
            // Path is available, you can build here
            Debug.Log("Path is available. You can build.");

            // Instantiate a building at the target position
            Instantiate(buildingPrefab, targetPosition.transform.position, Quaternion.identity);
        }
        else
        {
            // Path is not available, cannot build here
            Debug.Log("Path is not available. Cannot build.");
        }
    }

}
