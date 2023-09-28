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
    public List<Transform> target;

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
            target.Add(other.transform);
        }
    }


    private void Update()
    {
        
        if (target != null)        
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
        onDeath();

        if (target == null) 
        { 
            
        }
    }


    private void onDeath() 
    {
        if (ChooseTarget != null)
        {
            if (ChooseTarget.GetComponent<EnemyHealth>().health == 0)
            {
                target.Clear();
                ChooseTarget = null;
                storedDistance = Mathf.Infinity;
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        target.Clear();
        target.Remove(other.transform);
        ChooseTarget = null;
        storedDistance = Mathf.Infinity;
    }


}
