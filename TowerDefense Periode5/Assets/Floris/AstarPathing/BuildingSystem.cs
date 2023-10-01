using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    [Header("Script")]
    public Grid grid;
    public PathValidation pathValidation; // Reference to the PathValidation script
    public SpawnManager spawnManager;
    public BuildingSystem building;
    public Unit unit;

    [Header("Prefab")]
    public GameObject rocks;
    public GameObject[] towers;

    public GameObject startPosition;
    public GameObject targetPosition;
    
    public bool isTowerPlacingMode;
    public int selectedTowerIndex = 0;

    public Camera buildCam;
    // Update is called once per frame
    void Update()
    {

        if (isTowerPlacingMode)
        {
            HandleTowerPlacement();
        }
    }
    public void HandleTowerPlacement()
    {
        Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Node node = grid.NodeFromWorldPoint(hit.point);
            Vector3 buildingPosition = node.worldPosition;

            if (node != null && !node.walkable)
            {

                if (Keyboard.current.tKey.wasPressedThisFrame)
                {

                    selectedTowerIndex = (selectedTowerIndex + 1) % towers.Length;
                }


                if (Mouse.current.leftButton.wasPressedThisFrame)
                {

                    GameObject selectedTowerPrefab = towers[selectedTowerIndex];
                    Instantiate(selectedTowerPrefab, buildingPosition, Quaternion.identity);
                    isTowerPlacingMode = false;


                    if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    {
                        isTowerPlacingMode = false;
                    }
                }
            }
        }

    }
    public void BuilderRocks()
    {
        if (spawnManager.isBuildPhase)
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                Ray ray = buildCam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Node node = grid.NodeFromWorldPoint(hit.point);
                    Vector3 buildingPosition = node.worldPosition;

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
}



