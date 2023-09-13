using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System;

public class PathFinding : MonoBehaviour
{

    PathManager pathManager;
    Grid grid;
    public GameObject waypointMarkerPrefab;




    public void Awake()
    {
        grid = GetComponent<Grid>();
        pathManager = GetComponent<PathManager>();

    }
    
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
       
        print("PathFinding StartFindPath: Target Position: " + targetPos);
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] wayPoints = new Vector3[0];

        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
      
        if(startNode.walkable && targetNode.walkable)
        {
            print("Start node walkable: " + startNode.walkable);
            print("Target node walkable: " + targetNode.walkable);
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);





            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                print("Inside the while loop");
                if (currentNode == targetNode)
                {
                    print("Path found!");
                    wayPoints = RetracePath(startNode, targetNode);
                    pathSuccess = true;
                    print(pathSuccess);
                    foreach (Vector3 waypoint in wayPoints)
                    {
                        Instantiate(waypointMarkerPrefab, waypoint, Quaternion.identity);
                    }
                    break;

                }
                yield return null;
                foreach (Node neighbour in grid.CalculateNeighbours(currentNode))
                {
                    print("Processing neighbour: " + neighbour);
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
      
        

        yield return null;
        if (pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
            foreach (Vector3 waypoint in wayPoints)
            {
                print("Waypoint: " + waypoint);

                
            }
        }

        pathManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }



    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        
        Vector3[] waypoints = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            waypoints[i] = path[i].worldPosition;
        }

        
        System.Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 previousWaypoint = path[0].worldPosition;
        float waypointDistanceThreshold = 0.5f; 

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 direction = path[i].worldPosition - previousWaypoint;
            if (direction.magnitude > waypointDistanceThreshold)
            {
                waypoints.Add(path[i].worldPosition);
                previousWaypoint = path[i].worldPosition;
            }
        }

        return waypoints.ToArray();
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
