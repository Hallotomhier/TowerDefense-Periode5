using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;


    static PathManager instance;
    PathFinding PathFinding;
    public bool isProcessing;
    private void Awake()
    {
        instance = this;
        PathFinding = GetComponent<PathFinding>();
    }
    public static void PathRequesting(Vector3 pathStartPoint, Vector3 pathEndPoint, Action<Vector3[],bool> callBack)
    {
        PathRequest newRequest = new PathRequest(pathStartPoint,pathEndPoint,callBack);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }
    void TryProcessNext()
    {
        if (!isProcessing && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            PathFinding.StartFindPath(currentPathRequest.pathStartPoint, currentPathRequest.pathEndPoint);
        }
    }
    public void FinishedProcessingPath(Vector3[] path,bool finished)
    {
        currentPathRequest.callBack(path,finished);
        isProcessing = false;
        TryProcessNext();
    }
    

    
    struct PathRequest
    {
        public Vector3 pathStartPoint;
        public Vector3 pathEndPoint;
        public Action<Vector3[],bool> callBack;

        public PathRequest(Vector3 _start,Vector3 _end, Action<Vector3[], bool> _callBack)
        {
            pathStartPoint = _start;
            pathEndPoint = _end;
            callBack = _callBack;
        }
    }
}
