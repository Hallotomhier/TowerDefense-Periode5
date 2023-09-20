using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public Grid grid;
    public SpawnManager spawnManager;
    public GameObject buildingPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame && spawnManager.buildPhase)
        {
            spawnManager.buildPhase = !spawnManager.buildPhase;

            Cursor.visible = true;
            Cursor.visible = false;

            if (spawnManager.buildPhase)
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
