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
    public InstanceVogel birdTowerScript;
    public CannonTower cannonTowerScript;
    public WindMill windMillScript;


    public Vector3 buildingPos;
    [Header("Prefab")]
    public GameObject rocks;
    public GameObject windmill;
    public GameObject raft;
    public GameObject cannonTower;
    public GameObject startPosition;
    public GameObject targetPosition;
    public GameObject birdTower;

    [Header ("Bool")]
    public bool isTowerPlacingMode;
    public bool isRockPlacingMode;

    public bool spawnCannonTower = false;
    public bool spawnRocks = false;
    public bool spawnWindmill = false;
    public bool spawnRaft = false;
    public bool birdTowers = false;
    public bool upgradeTowers = false;
    
    public Camera buildCam;
    public float timer;
    public float delay;

    public string[] upgradeTags;
  

    private void Awake()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        if (spawnManager.isBuildPhase)
        {
            
            if (spawnRaft)
            {
                BuilderRaft();
             
            }
            else if (spawnRocks)
            {
                BuilderRocks();
                
            }
        }
        
        if (spawnCannonTower && isTowerPlacingMode ==true)
        {
            HandleCannonPlacement();
        }
        else if (spawnWindmill && isTowerPlacingMode == true)
        {
            HandleWindmillPlacement();
        }
        else if (birdTowers && isTowerPlacingMode == true)
        {
            HandleKamikazePlacement();
        }
        else if(upgradeTowers && isTowerPlacingMode == true)
        {
            UpgradeSystem();
        }
       

    }
 
    public void SpawnCannonTower()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = true;
        spawnWindmill = false;
        spawnRocks = false;
        spawnRaft = false;
        birdTowers = false;

    }

    public void SpawnWindmill()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = true;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = false;
    }

    public void SpawnRaft()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = true;
        spawnRocks = false;
        birdTowers = false;
    }

    public void SpawnRocks()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = true;
        birdTowers = false;
    }
    public void SpawnBirdTower()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = true;
    }

    public void UpgradeTowersCheck() 
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = false;
        upgradeTowers = true;
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

                if (node != null && !node.walkable && hit.collider.tag != ("Tower") || node != null && !node.walkable && hit.collider.CompareTag("Raft"))
                {



                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower
                      
                        Instantiate(cannonTower, buildingPosition, Quaternion.identity);
                        isTowerPlacingMode = true;
                        recources.wood -= 5;
                        recources.stone -= 2;
                        
                        delay = 2f;

                        //Cancel
                        
                    }
                }
            }
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
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

                if (node != null && !node.walkable && hit.collider.tag != ("Tower") || node != null && !node.walkable && hit.collider.CompareTag("Raft"))
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower

                        Instantiate(windmill,buildingPosition, Quaternion.identity);
                        isTowerPlacingMode = true;
                        recources.wood -= 5;
                        recources.stone -= 2;
                      
                        delay = 2f;

                        //Cancel
                       
                    }
                }
            }
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
        }

    }

    
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

                if (node != null && !node.walkable && hit.collider.tag != ("Tower")|| node != null && !node.walkable && hit.collider.CompareTag("Raft"))
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower

                        Instantiate(birdTower, hit.point, Quaternion.identity);
                        isTowerPlacingMode = true;
                        recources.wood -= 5;
                        recources.stone -= 2;

                        
                    }
                }
            }
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
        }
    }
    

    public void BuilderRocks()
    {
        if (recources.stone >= 1)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
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
                       
                        node.walkable = true;
                        bool validPathExists = pathValidation.IsPathValid(startPosition.transform.position, targetPosition.transform.position);

           

                        if (validPathExists)
                        {
                            GameObject newRock = Instantiate(rocks, buildingPosition, Quaternion.identity);
                            pathValidation.rocksAndRafts.Add(newRock);
                            node.walkable = false;
                            recources.stone -= 1;
                            delay = 0;
                        }
                        else
                        {
                            Debug.Log("noPath.");
                            node.walkable = true;
                        }
                    }

                }
            }

        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
        }
    }

    public void BuilderRaft()
    {
        if (recources.stone >= 1 && recources.wood >= 5)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                   
                    Node node = grid.NodeFromWorldPoint(hit.point);
                   
                    Vector3 buildingPosition = new Vector3(node.worldPosition.x, 4.3f, node.worldPosition.z);
                    Debug.Log("" + hit.collider.name);
                    if (node != null && node.walkable)
                    {

                      
                        node.walkable = false;
                        bool validPathExists = pathValidation.IsPathValid(startPosition.transform.position, targetPosition.transform.position);

                     

                        if (validPathExists)
                        {
                            GameObject newRaft = Instantiate(raft, buildingPosition, Quaternion.identity);
                            pathValidation.rocksAndRafts.Add(raft);
                            node.walkable = false;
                            recources.stone -= 1;
                            recources.wood -= 5;
                            delay = 0;

                        }
                        else
                        {
                            Debug.Log("noPath.");
                            node.walkable = true;
                        }
                    }

                }
            }

        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
        }

    }
    public void UpgradeSystem()
    {
        Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out hit))
            {


                Debug.LogError(hit.collider.name);

               
                if (upgradeTags.Contains(hit.collider.name))
                {
                    if (hit.collider.tag == "CannonTower" && cannonTowerScript.level != 2)
                    {
                        cannonTowerScript = hit.collider.GetComponent<CannonTower>();
                        cannonTowerScript.level++;
                    }
                    else if (hit.collider.tag == "WindMill" && windMillScript.level != 2)
                    {
                        windMillScript = hit.collider.GetComponent<WindMill>();
                        windMillScript.level++;
                    }
                    else if (hit.collider.tag == "BirdTower" && birdTowerScript.level != 2)
                    {
                        birdTowerScript = hit.collider.GetComponent<InstanceVogel>();
                        birdTowerScript.level++;
                    }
                }
            }
        }

            
            
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
        }
    }

}



