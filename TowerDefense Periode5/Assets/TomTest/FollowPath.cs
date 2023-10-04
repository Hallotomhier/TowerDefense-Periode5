using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform[] pathPos;
    private int nowPos = 0; 
    public float speed;
    public float hp;
    public TownHallHealth thh;
    
        
    void Start()
    {
        
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

        if(nowPos == 22)
        {
            thh.townHealth -= 1;
            Destroy(gameObject);
        }

        if (hp <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
