using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System;
using UnityEditor.Experimental.GraphView;
using System.IO;

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
    // start nieuw pathfinding process

    public bool PathAvailable(Vector3 startPos, Vector3 targetPos)
    {
        List<Vector3> path = (List<Vector3>)FindPath(startPos, targetPos);

        if (path != null && path.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode != null && targetNode != null && startNode.walkable && targetNode.walkable)
        {
            //print("PathFinding StartFindPath: Target Position: " + targetPos);
            StartCoroutine(FindPath(startPos, targetPos));
        }
        else
        {
            
           //print("Invalid start or target node.");
        }
    }
   
    
    public IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] wayPoints = new Vector3[0];

        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
      
        //controleert of startnode en end node walkable zijn
        if(startNode.walkable && targetNode.walkable)
        {
            //print("Start node walkable: " + startNode.walkable);
           // print("Target node walkable: " + targetNode.walkable);

            //openset zijn nodes die nog niet geprocessed zijn
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            //closedset is nodes die al geprocessed zijn en niks meer mee gebeurt hoeft te worden
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            // start  het a* pathfinding
            while (openSet.Count > 0)
            {
                // herhaalt totdat de openset leeg is of de doel node is berijkt
                Node currentNode = openSet.RemoveFirst();
                //als de openset node is voltooid stopt hij de node in de closed set
                closedSet.Add(currentNode);
                //print("Inside the while loop");

                if (currentNode == targetNode)
                {
                    //print("Path found!");
                    // hier heeft die het pad gevonden
                    wayPoints = RetracePath(startNode, targetNode);
                    pathSuccess = true;
                    print(pathSuccess);
                    //de foreach gaat ook weg is voor waypoint markers
                    foreach (Vector3 waypoint in wayPoints)
                    {
                        Instantiate(waypointMarkerPrefab, waypoint, Quaternion.identity);
                    }
                    break;

                }
                //loopt door de neighbournodes en voert a* uit
                yield return null;
                foreach (Node neighbour in grid.CalculateNeighbours(currentNode))
                {
                    //print("Processing neighbour: " + neighbour);
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
      
        
        //voltooid pathfinding en geeft resultaat door aan de pathmanager
        yield return null;
        if (pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
            foreach (Vector3 waypoint in wayPoints)
            {
               // print("Waypoint: " + waypoint);
                
                
            }
        }

        pathManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }
    
  

    // retraced path van eindpunt naar startpunt
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

        // draait pad om zodat je start bij startpunt en naar eindpunt gaat
        System.Array.Reverse(waypoints);

        return waypoints;
    }
    
    // berekent geschatte afstand tussen  de 2 nodes (voor volgorde nodes in openset)
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
