using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public float speedUpDown = 1;
    public float upDownDistance = 1;
    public Vector3 startPos;
    public Transform player;
    public float speed;

    public Camera playerCam;

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x, Mathf.Sin(Time.time * speedUpDown) + upDownDistance, startPos.z);
        
    }
    
        
}
