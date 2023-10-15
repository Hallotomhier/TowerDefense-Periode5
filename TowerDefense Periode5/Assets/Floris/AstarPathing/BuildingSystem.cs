using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.IO;
using System.Linq;

public class BuildingSystem : MonoBehaviour
{
    [Header("Script")]
    public Grid grid;
    public PathValidation pathValidation; 
    public SpawnManager spawnManager;
    public BuildingSystem building;
    public Unit unit;
    public Recources recources;
    public PathFinding pathfinding;

    [Header("Prefab")]
    public GameObject rocks;
    public GameObject windmill;
    public GameObject raft;
    public GameObject cannonTower;
    public GameObject startPosition;
    public GameObject targetPosition;

    [Header ("Bool")]
    public bool isTowerPlacingMode;
    public bool isRockPlacingMode;

    public bool spawnCannonTower = false;
    public bool spawnRocks = false;
    public bool spawnWindmill = false;
    public bool spawnRaft = false;
    
    public Camera buildCam;
    

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.isBuildPhase)
        {
            if (spawnCannonTower)
            {
                HandleCannonPlacement();

            }
            else if(spawnWindmill)
            {
                HandleWindmillPlacement();
            }
            else if (spawnRaft)
            {
                BuilderRaft();
            }
            else if (spawnRocks)
            {
                BuilderRocks();
            }
        }
        
        
    }

    public void SpawnCannonTower()
    {
        spawnCannonTower = true;
        spawnWindmill = false;
        spawnRocks = false;
        spawnRaft = false;

    }

    public void SpawnWindmill()
    {
        spawnCannonTower = false;
        spawnWindmill = true;
        spawnRaft = false;
        spawnRocks = false;
    }

    public void SpawnRaft()
    {
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = true;
        spawnRocks = false;
    }

    public void SpawnRocks()
    {
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = true;
    }

    public void HandleCannonPlacement()
    {
        Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if(recources.wood >= 5 && recources.stone >= 2)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Node node = grid.NodeFromWorldPoint(hit.point);
                Vector3 buildingPosition = node.worldPosition;

                if (node != null && !node.walkable)
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower
                      
                        Instantiate(cannonTower, hit.point, Quaternion.identity);
                        isTowerPlacingMode = false;
                        recources.wood -= 5;
                        recources.stone -= 2;

                        //Cancel
                        if (Keyboard.current.escapeKey.wasPressedThisFrame)
                        {
                            isTowerPlacingMode = false;
                        }
                    }
                }
            }
        }
       

    }

    public void HandleWindmillPlacement()
    {
        Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (recources.wood >= 5 && recources.stone >= 2)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Node node = grid.NodeFromWorldPoint(hit.point);
                Vector3 buildingPosition = node.worldPosition;

                if (node != null && !node.walkable /* && !hit.collider.CompareTag("Path")*/)
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower

                        Instantiate(windmill, hit.point, Quaternion.identity);
                        isTowerPlacingMode = false;
                        recources.wood -= 5;
                        recources.stone -= 2;

                        //Cancel
                        if (Keyboard.current.escapeKey.wasPressedThisFrame)
                        {
                            isTowerPlacingMode = false;
                        }
                    }
                }
            }
        }


    }

    /*
    public void HandleKamikazePlacement()
    {
        Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (recources.wood >= 5 && recources.stone >= 2)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Node node = grid.NodeFromWorldPoint(hit.point);
                Vector3 buildingPosition = node.worldPosition;

                if (node != null && !node.walkable && !hit.collider.CompareTag("Path"))
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower

                        Instantiate(, hit.point, Quaternion.identity);
                        isTowerPlacingMode = false;
                        recources.wood -= 5;
                        recources.stone -= 2;

                        //Cancel
                        if (Keyboard.current.escapeKey.wasPressedThisFrame)
                        {
                            isTowerPlacingMode = false;
                        }
                    }
                }
            }
        }


    }
    */

    public void BuilderRocks()
    {
        if (recources.stone >= 1)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    Vector3 buildingPosition = node.worldPosition;
                    Debug.Log("" + hit.collider.name);
                    if (node != null && node.walkable)
                    {
                        bool originalWalkable = node.walkable;
                        node.walkable = false;
                        bool validPathExists = pathValidation.IsPathValid(startPosition.transform.position, targetPosition.transform.position);

                        node.walkable = originalWalkable;

                        if (validPathExists)
                        {
                            Instantiate(rocks, buildingPosition, Quaternion.identity);
                            node.walkable = false;
                            recources.stone -= 1;
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

    public void BuilderRaft()
    {
        if (recources.stone >= 1 && recources.wood >= 5)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                   
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    Vector3 buildingPosition = node.worldPosition;
                    Debug.Log("" + hit.collider.name);
                    if (node != null && node.walkable)
                    {
                        bool originalWalkable = node.walkable;
                        node.walkable = false;
                        bool validPathExists = pathValidation.IsPathValid(startPosition.transform.position, targetPosition.transform.position);

                        node.walkable = originalWalkable;

                        if (validPathExists)
                        {
                            Instantiate(raft, buildingPosition, Quaternion.identity);
                            node.walkable = false;
                            recources.stone -= 1;
                            recources.wood -= 5;
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



