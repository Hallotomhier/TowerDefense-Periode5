using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speedUpDown = 1;
    public float upDownDistance = 1;
    public Vector3 startPos;
    public GameObject player;

   

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x, Mathf.Sin(Time.time * speedUpDown) + upDownDistance, startPos.z);
        RotateToPlayer();
    }
    public void RotateToPlayer()
    {
        Vector3 rotatePos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(rotatePos);
    }
        
}
