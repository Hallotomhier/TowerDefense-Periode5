using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3[] path;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unit Start: Target Position: " + target.position);
        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.blue;
        PathManager.PathRequesting(transform.position, target.position, OnPathFound);
    }
    
    
    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        float distanceThreshold = 0.2f; 
        float waitTimeBetweenWaypoints = 0f;
        if (path.Length == 0)
        {
            yield break;
        }
        
        for (int i = 0; i < path.Length; i++)
        {
            Vector3 currentWaypoint = path[i];

            while (Vector3.Distance(transform.position, currentWaypoint) >= distanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }

           
            yield return new WaitForSeconds(waitTimeBetweenWaypoints);
        }
    }
    
}
