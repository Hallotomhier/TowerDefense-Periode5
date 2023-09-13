using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    //queue om de pathfinding aanvragen in de wachtrij te zetten
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    //verwijzing naar de enige instantie van de pathmanager zorgt ervoor dat er maar 1 instantie wordt gemaakt van bepaalde class
    static PathManager instance;

    PathFinding PathFinding;
    public bool isProcessing;
    private void Awake()
    {

        instance = this;
        PathFinding = GetComponent<PathFinding>();
    }
    // start pathfinding aanvraag
    public static void PathRequesting(Vector3 pathStartPoint, Vector3 pathEndPoint, Action<Vector3[],bool> callBack)
    {
        //maakt nieuw pathrequest met start punt en eindpunt
        PathRequest newRequest = new PathRequest(pathStartPoint,pathEndPoint,callBack);
        //plaatst aanvraag in wachtrij
        instance.pathRequestQueue.Enqueue(newRequest);
        //probeert de volgende aanvraag teverwerken als er geen actief meer is
        instance.TryProcessNext();
    }
    //probeert volgende aanvraagt te verwerken
    void TryProcessNext()
    {
        if (!isProcessing && pathRequestQueue.Count > 0)
        {
            //haalt volgende aanvraag uit wachtrij
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            //start pathfinding voor huidige aanvraag
            PathFinding.StartFindPath(currentPathRequest.pathStartPoint, currentPathRequest.pathEndPoint);
        }
    }
    // wordt aangeroepen als pathfinding process voltooid is
    public void FinishedProcessingPath(Vector3[] path,bool finished)
    {
        //roept callbackfunctie van huidige aanvraag met gevonde pad
        currentPathRequest.callBack(path,finished);
        isProcessing = false;
        // probeert volgende aanvraag teverwerken
        TryProcessNext();
    }
    

    // struct die  pathfinding-aanvraag representeert. (container voor alle informatie)
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
