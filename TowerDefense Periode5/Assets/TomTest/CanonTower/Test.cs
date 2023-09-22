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

    public Test(Transform _target) 
    {
        _target = ChooseTarget;
    }


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
