using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VogelBoem : MonoBehaviour
{
    public GameObject target;
    public float flySpeed;
    public GameObject closestTarget;
    public ParticleSystem boem;
    private bool isPlayed = false;

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
            closestTarget = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(currentPosition, enemy.transform.position);
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
        }
    }




    void Update()
    {
        if (target != null)
        {
            Vector3 follow = target.transform.position;
            var speed = flySpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, follow, speed);
        }
        else 
        {
            Destroy(gameObject);
        }

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
                    Destroy(gameObject);
                }
            }
            
            
            
            
            
            
            /*if (!isPlayed)
            {
                Debug.Log("boem");
                boem.Play();
                if (!boem.isPlaying) 
                {
                    isPlayed = true;
                }
                
            }
            else 
            {
                Debug.Log("destroy");
                Destroy(gameObject);
            }*/
            
        }
    }
}
