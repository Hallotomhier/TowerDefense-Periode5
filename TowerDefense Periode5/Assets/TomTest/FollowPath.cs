using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public GameObject player;
    public Transform[] pathPos;
    public int nowPos = 0;
    
    public float speed;
        
    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (transform.position != pathPos[nowPos].position)
        {
            player.transform.position = Vector3.MoveTowards(transform.position, pathPos[nowPos].position,speed * Time.deltaTime);
            var q = Quaternion.LookRotation(pathPos[nowPos].position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, speed * Time.deltaTime);
        }
        else 
        {
            nowPos = (nowPos + 1) % pathPos.Length;
        }
        
    }
}
