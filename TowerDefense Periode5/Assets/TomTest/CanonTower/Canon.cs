using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("TargetSettings")]
    public Test detect;
    public Transform target;

    [Header("DamageSettings")]
    public float[] damage;
    public float[] delay;
    
    [Header("Level")]
    private float timer;
    public int level;
 

    void Update()
    {

        target = detect.ChooseTarget;
        
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > delay[level])
            {
                Shoot();
                timer -= delay[level];
            }
        }
        else 
        {
            timer = 0f;
        }            
    }

    private void Shoot() 
    {
        target.GetComponent<EnemyHealth>().health -= damage[level];
    }


} 
