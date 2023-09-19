using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public Grid grid;
    public GameObject buildingPrefab;

    private bool isBuildingMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            isBuildingMode = !isBuildingMode;
            if (isBuildingMode)
            {
           
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (isBuildingMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                Node node = grid.NodeFromWorldPoint(hit.point);
                if(node != null && node.walkable)
                {
                    Vector3 buildingPosition = node.worldPosition;
                    Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
                    node.walkable = false;
                }
            }
        }
    }
    
}
