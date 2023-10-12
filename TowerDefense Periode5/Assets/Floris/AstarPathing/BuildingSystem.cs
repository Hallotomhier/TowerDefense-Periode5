using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.IO;

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
    public GameObject[] towers;

    public GameObject startPosition;
    public GameObject targetPosition;
    
    public bool isTowerPlacingMode;
    public bool isRockPlacingMode;
    public int selectedTowerIndex = 0;
    public string[] towerNames;
    public Camera buildCam;
    RaycastHit hit;
    
    [Header("Vector3")]
    public Vector3 startClickPosition;
    public Vector3 endClickPoisition;

    [Header("Bool")]
    public bool isPathSucces;
    public List<Vector3> buildingPath;

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
            NewBuildingRockMode();
            //BuilderRocks();
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
    public void NewBuildingRockMode()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
           
            Ray ray =buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out hit))
            {
                FindPathBuilding(startClickPosition, endClickPoisition);
                if(isPathSucces && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Debug.Log(startClickPosition.x + startClickPosition.z);
                }
                
            }
          
        }
    }
    /*public void BuilderRocks()
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
    */
    public List<Node> FindPathBuilding(Vector3 startPos, Vector3 targetPos)
    {
       
        List<Node> pathBuilding = new List<Node>();
        Node startNodeBuilding = grid.NodeFromWorldPoint(startPos);
        Node targetNodeBuilding = grid.NodeFromWorldPoint(targetPos);

        if (startNodeBuilding != null && targetNodeBuilding != null && startNodeBuilding.walkable && targetNodeBuilding.walkable)
        {

            Heap<Node> openSetBuilding = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSetBuilding = new HashSet<Node>();
            openSetBuilding.Add(startNodeBuilding);

            while (openSetBuilding.Count > 0)
            {
                Node currentNode = openSetBuilding.RemoveFirst();
                closedSetBuilding.Add(currentNode);

                if (currentNode == targetNodeBuilding)
                {
                    isPathSucces = true;
                   
                    break;
                }

                foreach (Node neighbour in grid.CalculateNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSetBuilding.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + pathfinding.GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSetBuilding.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = pathfinding.GetDistance(neighbour, targetNodeBuilding);
                        neighbour.parent = currentNode;

                        if (!openSetBuilding.Contains(neighbour))
                            openSetBuilding.Add(neighbour);
                        else
                            openSetBuilding.UpdateItem(neighbour);
                    }
                }
            }
        }

        return pathBuilding;
    }
    public void ClickBuildMode()
    {
        Node node = grid.NodeFromWorldPoint(hit.point);
        startClickPosition = node.worldPosition;
    }
    public void ClickEndBuildmode()
    {
        Node node = grid.NodeFromWorldPoint(hit.point);
        endClickPoisition = node.worldPosition;
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



