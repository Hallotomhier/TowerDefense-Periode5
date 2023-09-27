using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    Unit unit;
    public Enemy enemy;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyComponent = other.GetComponent<Enemy>();

            if (enemyComponent != null)
            {
                enemyComponent.Check();
            }

           
            if (unit != null)
            {
                unit.MarkAsDestroyed();
            }

            Destroy(other.gameObject);
        }
    }
}

