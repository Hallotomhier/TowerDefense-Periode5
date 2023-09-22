using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VogelBoem : MonoBehaviour
{
    public Transform target;
    public float flySpeed;
    public InstanceVogel vogelSpawn;

    private void Awake()
    {
        vogelSpawn = GetComponent<InstanceVogel>();
        target = vogelSpawn.target;
    }

    public void Start()
    {
        
    }


    void Update()
    {
        
        if (target != null) 
        {
            Vector3 follow = target.position;
            var speed = flySpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, follow, speed);
        }
        
    }
}
