using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject target;
    public float speed;
    Vector3[] path;
    private bool isDestroyed = false;
    private Coroutine followPathCoroutine;
    public float heightOffset = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Target");
        if (target == null)
        {
            Debug.LogError("Target not found.");
            
        }

       
        PathManager.RequestPath(transform.position, target.transform.position, OnPathFound, true);
    }

    public void OnPathFound(List<Node> newPath, bool pathSuccess)
    {
        if (this == null || isDestroyed)
        {
            return; 
        }

        if (pathSuccess)
        {
            
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
        

        float distanceThreshold = 0.2f;

        if (path == null || path.Length == 0)
        {
            Debug.LogError("Path is null.");
            yield break;
        }

        Debug.Log("Starting FollowPath.");

        for (int i = 0; i < path.Length; i++)
        {
            if (isDestroyed)
            {
                yield break;
            }

            Vector3 currentWaypoint = path[i];
            Debug.Log("Moving to waypoint " + i + ": " + currentWaypoint);

            while (Vector3.Distance(transform.position, currentWaypoint) >= distanceThreshold)
            {
                if (isDestroyed)
                {
                    yield break;
                }
              
               
                transform.LookAt(currentWaypoint);
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                Debug.Log("Unit position: " + transform.position);
                yield return null;
            }

            Debug.Log("Reached waypoint " + i + ".");
            yield return new WaitForSeconds(0.2f);
        }

        
    }

    public void MarkAsDestroyed()
    {
        isDestroyed = true;

        if (followPathCoroutine != null)
        {
            StopCoroutine(followPathCoroutine);
        }
        
        Destroy(gameObject);
    }
}
