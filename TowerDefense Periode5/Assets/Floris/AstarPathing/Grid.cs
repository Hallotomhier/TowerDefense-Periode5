using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmo;
    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
  
    float nodeDiameter;
    int gridSizeX, gridSizeY;
  

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }


    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = IsWalkable(worldPoint);

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x, y].neighbours = CalculateNeighbours(grid[x, y]);
            }
        }
    }

    public List<Node> CalculateNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        
        return neighbours;
    }
    bool IsWalkable(Vector3 worldPosition)
    {
        RaycastHit hit;

        float waterSurfaceHeight = 0.9f;

        if (Physics.Raycast(worldPosition + Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity))
        {
            float terrainHeight = hit.point.y;

            

            if (terrainHeight > waterSurfaceHeight)
            {
                float walkableThreshold = waterSurfaceHeight + 1.6f;

                if (terrainHeight <= walkableThreshold)
                {
                    //Debug.Log("Terrain height: " + terrainHeight + " | Walkable Threshold: " + walkableThreshold);
                    return false;
                }
            }
        }

        return true;
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - (transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2);
        int x = Mathf.RoundToInt(localPosition.x / nodeDiameter);
        int y = Mathf.RoundToInt(localPosition.z / nodeDiameter);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmo)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * nodeDiameter);
                
            }
           
        }

    }
    
}