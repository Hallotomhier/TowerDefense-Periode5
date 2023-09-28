using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour
{
    Grid grid;
    PathManager pathManager;
    public BuildingSystem buildingSystem;
    public bool pathSuccess = false;

    public GameObject waypointMarkerPrefab;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathManager = GetComponent<PathManager>();
    }

    public void FindPathAsync(Vector3 startPos, Vector3 targetPos, Action<List<Node>> callback)
    {
        StartCoroutine(FindPathCoroutine(startPos, targetPos, callback));

        
   
    }

    private IEnumerator FindPathCoroutine(Vector3 startPos, Vector3 targetPos, Action<List<Node>> callback)
    {

        Debug.Log("Starting FindPathCoroutine");

        List<Node> path = FindPath(startPos, targetPos);

        if (path != null)
        {
            Debug.Log("Path Found");
            callback(path);
        }
        else
        {
            Debug.Log("No Path Found");
        }

        yield return null;
        buildingSystem.isCheckingPath = false;
      
        Debug.Log("Coroutine Complete");
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
       
        List<Node> path = new List<Node>();
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode != null && targetNode != null && startNode.walkable && targetNode.walkable)
        {
            
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    buildingSystem.isPathAvailable = true;
                    path = RetracePath(startNode, targetNode);
                    break;
                }

                foreach (Node neighbour in grid.CalculateNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        if (pathSuccess)
        {
            
            path = RetracePath(startNode, targetNode);
            Debug.Log(RetracePath(startNode, targetNode));
        }
       
        return path;
    }


    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();


        pathManager.FinishProcessingPath(path);
        return path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

    
}