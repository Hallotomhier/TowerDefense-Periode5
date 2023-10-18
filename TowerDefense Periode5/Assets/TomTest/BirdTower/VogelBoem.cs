using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VogelBoem : MonoBehaviour
{
    private GameObject self;
    public GameObject target;
    public float flySpeed;
    public GameObject closestTarget;
    public ParticleSystem boem;
   
    public bool isExploding = false;
    public float timer;
    public float delay = 0.1f;
    

    

    public void Start()
    {
        SearchForEnemies();
                
    }

    private void SearchForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
           
            float closestDistance = Mathf.Infinity;
           
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = enemy;
                }
            }
            target = closestTarget;
            

        }
        else 
        {
            target = null;
            StartCoroutine(DestroyWhenNoEnemys());
        }
    }




    void Update()
    {
        

        if (target != null)
        {
            
            var speed = flySpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
        if (target == null)
        {
            SearchForEnemies();
        }
        
        if(target != null && Vector3.Distance(transform.position,target.transform.position) < 2.0f)
        {
            if (!isExploding)
            {
                boem.Play();
                isExploding = true;
                StartCoroutine(ExplodeAfterDelay());
            }
        }

        /*
        if ((gameObject.transform.position - target.transform.position).magnitude < 2.0f) 
        {

            timer += Time.deltaTime;
            if (!isPlayed) 
            {
                boem.Play();
                if (timer >= delay)
                {
                    timer -= delay;
                    isPlayed = true;
                    target.GetComponent<EnemyHealth>().health -= 50;
                    Destroy(gameObject);
                }
            }     
            
        }
       */
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        if (target != null)
        {
            var enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.health -= 50;
            }
        }
        isExploding = false;
        SearchForEnemies();
        Destroy(gameObject);
    }
    private IEnumerator DestroyWhenNoEnemys()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
