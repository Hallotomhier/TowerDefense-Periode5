using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<Transform> target;
    public Transform tower;
    public float storedDistance;
    public Transform ChooseTarget;


    private void Start()
    {
        storedDistance = Mathf.Infinity;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            target.Add(other.transform);
        }
    }


    private void Update()
    {
        foreach (var target in target) 
        {

           float distance = Vector3.Distance(tower.position, target.position);
            if (distance < storedDistance) 
            {
                storedDistance = distance;
                ChooseTarget = target;
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target.Remove(other.transform);
        ChooseTarget = null;
        storedDistance = Mathf.Infinity;
    }


}
