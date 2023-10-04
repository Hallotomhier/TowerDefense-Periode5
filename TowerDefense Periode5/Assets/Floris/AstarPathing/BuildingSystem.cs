using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    [Header("Script")]
    public Grid grid;
    public PathValidation pathValidation; 
    public SpawnManager spawnManager;
    public BuildingSystem building;
    public Unit unit;
    public Recources recources;

    [Header("Prefab")]
    public GameObject rocks;
    public GameObject[] towers;

    public GameObject startPosition;
    public GameObject targetPosition;
    
    public bool isTowerPlacingMode;
    public bool isRockPlacingMode;
    public int selectedTowerIndex = 0;
    public string[] towerNames;
    public Camera buildCam;

    public TMP_Text currentTowerText;
    // Update is called once per frame
    void Update()
    {

        if (isTowerPlacingMode)
        {
            HandleTowerPlacement();
            
        }
        if (isRockPlacingMode)
        {
            BuilderRocks();
        }
    }
    public void HandleTowerPlacement()
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

                    if (Keyboard.current.tKey.wasPressedThisFrame)
                    {
                        //switch index
                        selectedTowerIndex = (selectedTowerIndex + 1) % towers.Length;

                    }
                    currentTowerText.text = towerNames[selectedTowerIndex];

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        //place tower
                        GameObject selectedTowerPrefab = towers[selectedTowerIndex];
                        Instantiate(selectedTowerPrefab, buildingPosition, Quaternion.identity);
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
                        bool originalWalkable = node.walkable;
                        //node.walkable = false;
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
    public void Towers()
    {
        if (spawnManager.isBuildPhase)
        {
            isTowerPlacingMode = true;
        }

    }
    public void Rocks()
    {
        if (spawnManager.isBuildPhase)
        {
            isRockPlacingMode = true;
        }
    }
}



