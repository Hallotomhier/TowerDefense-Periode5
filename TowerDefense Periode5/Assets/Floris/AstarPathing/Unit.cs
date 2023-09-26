using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject target;
    public float speed;
    Vector3[] path;
    

    // Start is called before the first frame update
    void Start()
    {
        

        target = GameObject.FindWithTag("Target");
        if (target == null)
        {
            Debug.LogError("Target object not found.");
            return;
        }
        
        Debug.Log("Requesting path from " + transform.position + " to " + target.transform.position);
        PathManager.RequestPath(transform.position, target.transform.position, OnPathFound, true);
    }

    public void OnPathFound(List<Node> newPath, bool pathSuccess)
    {
        Debug.Log("onpathfound Function.");
        if (pathSuccess)
        {
           
            Debug.Log("Path found. Length: " + newPath.Count);
            Vector3[] newPathVector3 = newPath.ConvertAll(node => node.worldPosition).ToArray();
            path = newPathVector3;
            StartCoroutine(FollowPath());
        }
        else
        {
            Debug.LogError("No path found");
          
        }
    }

    IEnumerator FollowPath()
    {
        Debug.Log("FollowPath started.");

        float distanceThreshold = 0.2f;

        if (path == null || path.Length == 0)
        {
            Debug.LogError("Path is null.");
            yield break;
        }

        Debug.Log("Starting FollowPath.");

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 currentWaypoint = path[i];
            Debug.Log("Moving to waypoint " + i + ": " + currentWaypoint);

            while (Vector3.Distance(transform.position, currentWaypoint) >= distanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                Debug.Log("Unit position: " + transform.position);
                yield return null;
            }

            Debug.Log("Reached waypoint " + i + ".");
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("FollowPath finished.");
    }



}
