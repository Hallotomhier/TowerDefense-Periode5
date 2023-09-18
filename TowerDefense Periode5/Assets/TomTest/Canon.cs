using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private RaycastHit hit;
    public Test detect;
    public Transform target;
    public float damage = 1;
    public Camera cam;
    private Vector3 tPos;





    void Start()
    {
        
    }

    
     

    void Update()
    {
        target = detect.ChooseTarget;
        if(target != null)
        {
            shoot();
            tPos = target.position - cam.transform.position;
        }
    }

    public void shoot() 
    {
        Debug.Log("test");
        Debug.DrawRay(cam.transform.position, tPos);
        if (Physics.Raycast(cam.transform.position, tPos, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Enemy")) 
            {
                Debug.Log(hit.transform.name);
            }

            /*Debug.Log("HIT");
            EnemyHealth enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth.health >= 0 && enemyHealth != null)
            {
                enemyHealth.health -= damage * Time.deltaTime;
            }
            else 
            {
                Debug.Log("no health"); 
            }*/

        }

    }
}
