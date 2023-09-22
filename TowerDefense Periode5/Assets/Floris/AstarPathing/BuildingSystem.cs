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

    public GameObject startPosition;
    public GameObject targetPosition;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool IsReachable(Vector3 startPos, Vector3 targetPos)
    {
        return  pathFinding.PathAvailable(startPos,targetPos);
    }

    void Update()
    {
        Vector3 startPos = startPosition.transform.position;
        Vector3 targetPos = targetPosition.transform.position;
        BuilderRocks(startPos,targetPos);
       
        
    }
    public void BuilderRocks( Vector3 startPos, Vector3 targetPos)
    {
        if (spawnManager.isBuildPhase && IsReachable(startPos, targetPos)) 
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    if (node != null && node.walkable)
                    {
                        Vector3 buildingPosition = node.worldPosition;
                        Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
                        node.walkable = false;
                    }
                }


            }
        }
    }
    
    
}
