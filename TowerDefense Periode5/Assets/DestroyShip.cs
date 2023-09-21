using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShip : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyComponent = other.GetComponent<Enemy>();

            if (enemyComponent != null)
            {
               
                enemyComponent.OnDestroy();
            }

            Destroy(other.gameObject);
        }
    }
}
