using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private RaycastHit hit;
    public Test detect;
    public Transform target;
    public int damage = 1;

    public GameObject self;
    
    public Camera cam;
    void Start()
    {
        
    }

    
     

    void Update()
    {

        target = detect.ChooseTarget;

        if (target != null)
        {
            shoot();
        }
        else 
        {
            Debug.Log("cant shoot");
        }




        

    }

    public void shoot() 
    {
        if (Physics.Raycast(self.transform.forward, target.position, out hit, Mathf.Infinity))
        {
            
            EnemyHealth enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.health -= damage;
            }
            Debug.DrawRay(self.transform.forward, target.position);

        }

    }
}
