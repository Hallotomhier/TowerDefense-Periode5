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
                        bool isValidPath = CheckValidPath(startPosition.transform.position, targetPosition.transform.position);

                        if (isValidPath)
                        {
                         
                            Vector3 buildingPosition = node.worldPosition;

                          
                            isCheckingPath = true;
                            pathFinding.FindPathAsync(startPosition.transform.position, targetPosition.transform.position, path => OnPathAvailableCheck(path, node));
                        }
                        else
                        {
                           
                            Debug.Log("is blocked.");
                        }
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
            Debug.Log("You can build.");
            Instantiate(buildingPrefab, node.worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Cannot build.");
        }
    }

    private bool CheckValidPath(Vector3 startPos, Vector3 targetPos)
    {
        
        List<Node> path = pathFinding.FindPath(startPos, targetPos);

        return path != null && path.Count > 0;
    }

}
