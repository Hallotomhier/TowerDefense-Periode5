using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Test detect;
    public Transform target;
    public float damage = 1;
    public float delay = 3f;
    public float timer;
    

    public string[] debug;





    void Start()
    {
        
    }

    
     

    void Update()
    {

        target = detect.ChooseTarget;
        
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                Shoot();
                timer -= delay;
            }
        }
        else 
        {
            timer = 0f;
        }            
    }

    private void Shoot() 
    {
        target.GetComponent<EnemyHealth>().health -= damage;
    }


} 
