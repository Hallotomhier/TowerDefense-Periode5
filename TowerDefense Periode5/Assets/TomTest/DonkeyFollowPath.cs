using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DonkeyFollowPath : MonoBehaviour
{
    public Recources recources;
    private int numberOfWaypoints = 31;
    public Transform[] pathPos;
    public Transform front;
    
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

    private void Awake()
    {
        
    }

    void Update()
    {

        if (transform.position != pathPos[nowPos].position)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, pathPos[nowPos].position, speed * Time.deltaTime);
            transform.LookAt(pathPos[nowPos]);
            //var targetRotation = Quaternion.LookRotation(pathPos[nowPos].position - front.transform.position);
            //transform.rotation = Quaternion.Slerp(front.transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            
            nowPos = (nowPos + 1) % pathPos.Length;
        }

    }
}
