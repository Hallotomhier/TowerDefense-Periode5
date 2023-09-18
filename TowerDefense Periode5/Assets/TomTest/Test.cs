using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<Transform> target;
    public Transform tower;
    public float storedDistance;
    public Transform ChooseTarget;
    public EnemyHealth enemyPlayer;

    
    private void Start()
    {
        storedDistance = Mathf.Infinity;
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            target.Add(other.transform);
        }
    }


    private void Update()
    {
        onDeath();
        if (target == null)        {
            Debug.Log("target not found");
        }
        else 
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

        
    }


    private void onDeath() 
    {
        if (ChooseTarget != null) 
        {
            if (ChooseTarget.GetComponent<EnemyHealth>().health <= 0)
            {
                target.Clear();
                ChooseTarget = null;
                storedDistance = Mathf.Infinity;
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
