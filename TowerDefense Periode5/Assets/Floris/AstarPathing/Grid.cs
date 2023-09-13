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
        // berekent diameter knoop punt
        nodeDiameter = nodeRadius * 2;
        // berekent aantal nodes in de x- en y- richting op  basis van node diameter en gridworldsize
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        // create grid wanner object geinitialiseerd wordt
        CreateGrid();
    }

     // berekent max aantal nodes en stuurt info terug
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

        // loopt door het grid en maakt nodes aan
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // berekent wereldpositie van huidige node
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                // checkt of node walkable is
                bool walkable = IsWalkable(worldPoint);

                // maakt nieuw node -exemplaar aan en slaat dit op
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
        // berekent en stelt de neighbournodes in grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x, y].neighbours = CalculateNeighbours(grid[x, y]);
            }
        }
    }

    // berekent en stuurt de buren van huidige node terug
    public List<Node> CalculateNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        //loopt door omliggende nodes ook diagonaal
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // slaat huidige node over
                if (x == 0 && y == 0)
                {
                    continue;
                }

                // berekent de grid coordinaties van neighbouring nodes
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                // controleet of buren binnen de grens van grid zijn
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        
        return neighbours;
    }
    // controleert of gegeven wereldpositie bereikbaar is
    bool IsWalkable(Vector3 worldPosition)
    {
        RaycastHit hit;

        // hoogte watteropppervlak
        float waterSurfaceHeight = 0.9f;

        //berekent terrain hoogte
        if (Physics.Raycast(worldPosition + Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity))
        {
            float terrainHeight = hit.point.y;

            
            //controleert of terrainhoogte boven treshhoold ligt
            if (terrainHeight > waterSurfaceHeight)
            {
                float walkableThreshold = waterSurfaceHeight + 1.6f;
                // als terrain hoogte boven de tresh hold is. dan is het niet walkable
                if (terrainHeight <= walkableThreshold)
                {
                  
                    return false;
                }
            }
        }
        // als terrain niet wordt geraakt is het walkable
        return true;
    }
    // vind en stuurt de node in het grid dat overeenkomt met een gegeven wereldpositie
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - (transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2);
        int x = Mathf.RoundToInt(localPosition.x / nodeDiameter);
        int y = Mathf.RoundToInt(localPosition.z / nodeDiameter);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }
    // dit gaat weg
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