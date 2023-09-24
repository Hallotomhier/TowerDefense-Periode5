using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject target;
    public float speed;
    Vector3[] path;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Target");
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.blue;

        // Request a path from PathManager
        PathManager.RequestPath(transform.position, target.transform.position, OnPathFound, true); // Pass 'true' for the isUnit parameter
    }

    public void OnPathFound(List<Node> newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            Vector3[] newPathVector3 = newPath.ConvertAll(node => node.worldPosition).ToArray();
            path = newPathVector3;
            StartCoroutine(FollowPath());
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
