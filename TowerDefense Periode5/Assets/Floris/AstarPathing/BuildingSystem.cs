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
    [Header("Prefab")]
    public GameObject buildingPrefab;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool IsReachable(Vector3 startPos, Vector3 targetPos)
    {
        return pathFinding.PathAvailable(startPos, targetPos);
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            

            Cursor.visible = true;
            Cursor.visible = false;

            if (spawnManager)
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
