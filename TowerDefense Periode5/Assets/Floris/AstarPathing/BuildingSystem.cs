using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    [Header("Script")]
    public Grid grid;
    public PathValidation pathValidation; // Reference to the PathValidation script
    public SpawnManager spawnManager;
    public BuildingSystem building;
    public Unit unit;

    [Header("Prefab")]
    public GameObject buildingPrefab;

    public bool isPathAvailable = false;
    public bool isCheckingPath;
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

                      
                        bool originalWalkable = node.walkable;

                  
                        node.walkable = false;

                       
                        bool validPathExists = pathValidation.IsPathValid(startPosition.transform.position, targetPosition.transform.position);

                        node.walkable = originalWalkable;

                        if (validPathExists)
                        {
    
                            Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
                            node.walkable = false;
                        }
                        else
                        {
                            Debug.Log("noPath.");
                        }
                    }
                }
            }
        }
    }



}
