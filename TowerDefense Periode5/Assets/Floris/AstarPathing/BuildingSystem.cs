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
        // check buildphase
        if (spawnManager.isBuildPhase)
        {
            //input
            if (Keyboard.current.bKey.wasPressedThisFrame && !isCheckingPath)
            {
                //raycast
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                //raycast hit
                if (Physics.Raycast(ray, out hit))
                {
                    //hitpoint takes nodefrom worldpos
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    if (node != null && node.walkable)
                    {
                        //buildpos = node in world[pos
                        Vector3 buildingPosition = node.worldPosition;

                        //checkt of path true is en deze bestaad
                        isCheckingPath = true;
                        pathFinding.FindPathAsync(startPosition.transform.position, targetPosition.transform.position, (path) => OnPathAvailableCheck(path, node));




                    }
                }
            }
        }
    }
    private void OnPathAvailableCheck(List<Node> path, Node node)
    {
       
        isCheckingPath = false;

        if (path != null && path.Count > 0)
        {
           
            Debug.Log("Path is available. You can build.");
        
            Instantiate(buildingPrefab,node.worldPosition , Quaternion.identity);
         
        }
        else
        {
            Debug.Log("Path is not available. Cannot build.");
        }
    }

}
