using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float storedDistance;

    [Header("Transform")]
    public Transform tower;
    public Transform ChooseTarget;

    [Header("Enemys")]
    public List<Transform> targets;

    [Header("Scripts")]
    public EnemyHealth enemyPlayer;

    


    private void Start()
    {
        storedDistance = Mathf.Infinity;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            targets.Remove(other.transform);
            if (ChooseTarget == other.transform) 
            {
                ChooseTarget = null;
                storedDistance = Mathf.Infinity;
            }
        }
    }


    private void Update()
    {

        if (ChooseTarget == null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                if (target != null) 
                {
                    float distance = Vector3.Distance(tower.position, target.position);
                    if (distance < storedDistance)
                    {
                        storedDistance = distance;
                        ChooseTarget = target;
                    }
                }
                break;
            }
        }

        if (ChooseTarget != null) 
        {
            EnemyHealth enemyHealth = ChooseTarget.GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemyHealth.health == 0) 
            {
                targets.Remove(ChooseTarget);
                ChooseTarget = null;
                storedDistance = Mathf.Infinity;
            }
        }
        
    }


    
    


}
