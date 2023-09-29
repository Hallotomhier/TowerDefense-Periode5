using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathValidation : MonoBehaviour
{
    public PathFinding pathFinding;

    public bool IsPathValid(Vector3 startPos, Vector3 targetPos)
    {

        List<Node> path = pathFinding.FindPath(startPos, targetPos);
        if(path.Count > 0)
        {
            return true;
        }
        return false;
    }
}

