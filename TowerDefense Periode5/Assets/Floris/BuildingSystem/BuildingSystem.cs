using UnityEngine;
using UnityEngine.InputSystem;



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
    public Tutorial tutorial;
    public SoundManager soundManager;
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
    public GameObject lastPlaced;

    private void Awake()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        if (spawnManager.isWaveActive == false)
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
        else if(upgradeTowers && isTowerPlacingMode == true)
        {
            UpgradeSystem();
        }
       

    }
 
    public void SpawnCannonTower()
    {
        tutorial.tutorialStep1 = false;
        tutorial.tutorialStep2 = true;
        isTowerPlacingMode = true;
        spawnCannonTower = true;
        spawnWindmill = false;
        spawnRocks = false;
        spawnRaft = false;
        birdTowers = false;
        if (recources.wood <= 5 && recources.stone <= 2)
        {
            soundManager.PlaySfx("No Money");
        }

    }

    public void SpawnWindmill()
    {
        tutorial.tutorialStep1 = false;
        tutorial.tutorialStep2 = true;
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = true;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = false;
        if (recources.wood <= 2 && recources.stone <= 5)
        {
            soundManager.PlaySfx("No Money");
        }
    }

    public void SpawnRaft()
    {
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = true;
        spawnRocks = false;
        birdTowers = false;
        if (recources.wood <= 5 && recources.stone <= 1)
        {
            soundManager.PlaySfx("No Money");
        }
    }

    public void SpawnRocks()
    {
        tutorial.tutorialStep2 = false;
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = true;
        birdTowers = false;
        if (recources.stone <= 1)
        {
            soundManager.PlaySfx("No Money");
        }
    }
    public void SpawnBirdTower()
    {
        tutorial.tutorialStep1 = false;
        tutorial.tutorialStep2 = true;
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = true;
    }

    public void UpgradeTowersCheck() 
    {
        tutorial.tutorialStep3 = false;
        tutorial.tutorialStep4 = true;
        isTowerPlacingMode = true;
        spawnCannonTower = false;
        spawnWindmill = false;
        spawnRaft = false;
        spawnRocks = false;
        birdTowers = false;
        upgradeTowers = true;
        if(recources.wood <= 10 && recources.stone <= 10)
        {
            soundManager.PlaySfx("No Money");
        }
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

                if (node != null && !node.walkable && hit.collider.tag != ("CannonTower") && hit.collider.tag != ("WindMill") || node != null && !node.walkable && hit.collider.CompareTag("Raft") && hit.collider.tag != ("CannonTower") && hit.collider.tag != ("WindMill"))
                {



                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower
                      
                        Instantiate(cannonTower, buildingPosition, Quaternion.identity);
                        isTowerPlacingMode = true;
                        recources.wood -= 5;
                        recources.stone -= 2;
                       
                        
                        delay = 2f;

                       
                        
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

                if (node != null && !node.walkable && hit.collider.tag != ("CannonTower") && hit.collider.tag != ("WindMill") || node != null && !node.walkable && hit.collider.CompareTag("Raft") && hit.collider.tag != ("CannonTower") && hit.collider.tag != ("WindMill"))
                {


                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower

                        Instantiate(windmill,buildingPosition, Quaternion.identity);
                        isTowerPlacingMode = true;
                        recources.wood -= 3;
                        recources.stone -= 3;
                        tutorial.tutorialStep2 = true;

                        delay = 2f;

                       
                    }
                }
            }
        }
      

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isTowerPlacingMode = false;
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

                if (node != null && !node.walkable && hit.collider.tag != ("CannonTower") && hit.collider.tag != ("WindMill") || node != null && !node.walkable && hit.collider.CompareTag("Raft") && hit.collider.tag != ("CannonTower") && hit.collider.tag !=("WindMill"))
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
    */
    

    public void BuilderRocks()
    {
        if (recources.stone >= 1 && Mouse.current.leftButton.isPressed)
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

                        if (validPathExists == true &&  hit.collider.tag != "Maze")
                        {
                            GameObject newRock = Instantiate(rocks, buildingPosition, Quaternion.identity);
                            pathValidation.rocksAndRafts.Add(newRock);
                            lastPlaced = newRock;
                            node.walkable = false;
                            validPathExists = false;
                            recources.stone -= 1;
                            delay = 0;
                            tutorial.tutorialStep3 = true;
                    }
                        else if (validPathExists == false)
                        {
                           
                            Debug.Log("noPath.");
                            node.walkable = true;
                            
                            
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

                     

                        if (validPathExists && hit.collider.tag != ("Maze"))
                        {
                            GameObject newRaft = Instantiate(raft, buildingPosition, Quaternion.identity);
                            pathValidation.rocksAndRafts.Add(raft);
                            node.walkable = false;
                            recources.stone -= 1;
                            recources.wood -= 5;
                            delay = 0;
                            tutorial.tutorialStep3 = true;

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
                if (hit.collider.CompareTag("CannonTower")) /*&& cannonTowerScript.level != 2)*/
                {
                    cannonTowerScript = hit.collider.GetComponent<CannonTower>();
                    if (cannonTowerScript != null && recources.wood >= 10 && recources.stone >= 10)
                    {
                        recources.wood -= 10;
                        recources.stone -= 10;
                        tutorial.tutorialStep4 = true;
                        cannonTowerScript.level++;
                        cannonTowerScript.UpgradeSystem();
                    }
                   

                }
                else if (hit.collider.CompareTag("WindMill"))/*&& windMillScript.level != 2)*/
                {
                    windMillScript = hit.collider.GetComponent<WindMill>();
                    if(windMillScript != null && recources.wood >= 10 && recources.stone >= 10)
                    {
                        recources.wood -= 10;
                        recources.stone -= 10;
                        tutorial.tutorialStep4 = true;
                        windMillScript.level++;
                        windMillScript.UpgradeSystem();
                    }
                   

                }
                else if (hit.collider.CompareTag("BirdTower")) /* && birdTowerScript.level != 2)*/
                {
                    birdTowerScript = hit.collider.GetComponent<InstanceVogel>();
                    if(birdTowerScript != null && recources.wood >=10 && recources.stone >= 10)
                    {
                        recources.wood -= 10;
                        recources.stone -= 10;
                        tutorial.tutorialStep4 = true;
                        birdTowerScript.level++;
                        birdTowerScript.UpgradeSystemBird();
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



