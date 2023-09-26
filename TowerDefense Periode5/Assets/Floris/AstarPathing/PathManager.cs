using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathManager instance;
    PathFinding pathFinding;
    bool isProcessing;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 pathStartPoint, Vector3 pathEndPoint, Action<List<Node>, bool> callback, bool isUnit)
    {
        PathRequest newRequest = new PathRequest(pathStartPoint, pathEndPoint, callback, isUnit);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();

        
       // laatste debug
    }

    void TryProcessNext()
    {
        if (!isProcessing && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            pathFinding.FindPath(currentPathRequest.pathStartPoint, currentPathRequest.pathEndPoint);
        }
    }

    public void FinishProcessingPath(List<Node> path)
    {
        currentPathRequest.callback(path, currentPathRequest.isUnit);
        isProcessing = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStartPoint;
        public Vector3 pathEndPoint;
        public Action<List<Node>, bool> callback;
        public bool isUnit;

        public PathRequest(Vector3 start, Vector3 end, Action<List<Node>, bool> cb, bool unit)
        {
            pathStartPoint = start;
            pathEndPoint = end;
            callback = cb;
            isUnit = unit;
        }
    }
}
