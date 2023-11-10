using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
   
    public Transform[] pathPos;
    private int nowPos = 0;
    public float speed = 5;
    public float hp;
    public float oldHP;
    public TownHallHealth thh;
    private int numberOfWaypoints = 26;
    public Recources recources;
    public RaycastHit hit;
    public GameObject empty;


    void Start()
    {
       

        oldHP = hp;
        recources = GameObject.Find("BuildManager").GetComponent<Recources>();
        thh = GameObject.FindWithTag("TownHall").GetComponent<TownHallHealth>();

       
        GameObject waypointsParent = GameObject.Find("EnemyWalkAblePath");

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
            gameObject.transform.position = Vector3.MoveTowards(transform.position, pathPos[nowPos].position,speed * Time.deltaTime);
            var targetRotation = Quaternion.LookRotation(pathPos[nowPos].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else 
        {
            nowPos = (nowPos + 1) % pathPos.Length;
        }

        if(nowPos == 26)
        {
            thh.townHealth -= 10;
            Destroy(gameObject);
        }

        if (hp <= 0) 
        {
            recources.wood += 3;
            recources.stone += 3;
            Destroy(gameObject);
        }
       
    }
}
