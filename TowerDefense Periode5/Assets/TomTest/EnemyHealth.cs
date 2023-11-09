using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int oldHealth;
    public Unit unit;
    public SoundManager soundManager;
    private void Awake()
    {
        oldHealth = health;
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        unit = GetComponent<Unit>();
    }
    public void Update()
    {
      
       if(health < oldHealth)
       {
            soundManager.PlaySfx("ImpactBoat");
            oldHealth = health;
       }
       if(health <= 1)
       {
            unit.MarkAsDestroyed();
       }
    }

}
