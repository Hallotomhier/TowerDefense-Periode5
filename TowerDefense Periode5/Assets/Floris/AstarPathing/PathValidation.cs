using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathValidation : MonoBehaviour
{
    public BuildingSystem buildingSystem;
    public PathFinding pathFinding;
    public List<GameObject> rocksAndRafts;
    public Grid grid;
    public bool IsPathValid(Vector3 startPos, Vector3 targetPos)
    { 

        List<Node> path = pathFinding.FindPath(startPos, targetPos);
        if(path.Count > 0)
        {
            return true;
        }
        else
        {
            Node node = grid.NodeFromWorldPoint(buildingSystem.lastPlaced.transform.position);
            node.walkable = true;
            Destroy(buildingSystem.lastPlaced);
            
        }

        foreach (var obstacle in rocksAndRafts)
        {
           
            path = pathFinding.FindPath(startPos, targetPos);
            
            if(path.Count > 0)
            {
                
                return true;
            }
          
            
           
        }
        return false;
    }
}

