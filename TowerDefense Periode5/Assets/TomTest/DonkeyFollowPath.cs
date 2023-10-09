using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyFollowPath : MonoBehaviour
{
    public Recources recources;
    private int numberOfWaypoints = 31;
    public Transform[] pathPos;
    private int nowPos = 0;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        GameObject waypointsParent = GameObject.Find("DonkeyPath");

        if (waypointsParent != null)
        {
            numberOfWaypoints = waypointsParent.transform.childCount;

            pathPos = new Transform[numberOfWaypoints];
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                pathPos[i] = waypointsParent.transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("WaypointsParent not found.");
        }
    }

    
    void Update()
    {

        if (transform.position != pathPos[nowPos].position)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, pathPos[nowPos].position, speed * Time.deltaTime);
            var targetRotation = Quaternion.LookRotation(pathPos[nowPos].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            
            nowPos = (nowPos + 1) % pathPos.Length;
        }

    }
}
