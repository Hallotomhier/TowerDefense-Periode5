using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotattiewindmill : MonoBehaviour
{
    public float speed;
    public bool canRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            gameObject.transform.Rotate(0, 0, 1 * speed *Time.deltaTime);
        }
       
        
    }
}
