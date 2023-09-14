using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectShip : MonoBehaviour
{
    private bool isDetect;
    private Vector3 center;
    public int range;
    public Transform midden;
    public List<Transform> detect;
   
    

    void Start()
    {
        center = gameObject.transform.position;
    }

    
    void Update()
    {
       
        Collider[] hitColliders = Physics.OverlapSphere(center  , range);
        foreach (var hitCollider in hitColliders) 
        {
            
            if (hitCollider != gameObject) 
            {
               
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(center, range);
    }
}
