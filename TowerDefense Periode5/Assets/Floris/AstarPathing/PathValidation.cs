using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathValidation : MonoBehaviour
{
    public PathFinding pathFinding;
    public List<GameObject> rocksAndRafts;

    public bool IsPathValid(Vector3 startPos, Vector3 targetPos)
    { 

        List<Node> path = pathFinding.FindPath(startPos, targetPos);
        if(path.Count > 0)
        {
            return true;
        }
        
        foreach(var obstacle in rocksAndRafts)
        {
            obstacle.SetActive(false);
            path = pathFinding.FindPath(startPos, targetPos);

            if(path.Count > 0)
            {
                obstacle.SetActive(true);
                return true;
            }
            obstacle.SetActive(true);
        }
        return false;
    }
}

