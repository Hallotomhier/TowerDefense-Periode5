using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private void Update()
    {
     if (health <= 1) 
     {
            Destroy(gameObject);       
     }
    }

}
