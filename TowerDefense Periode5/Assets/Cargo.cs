using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public List<GameObject> cargoList;
    public DestroyShip destroyShipScript;
    public void Awake()
    {
        destroyShipScript = GameObject.FindWithTag("Trigger").GetComponent<DestroyShip>();
    }
    public void AddSpawnList()
    {
        foreach(GameObject go in cargoList.ToArray())
        {
            destroyShipScript.spawnList.Add(go);
            cargoList.Remove(go);
        }
    }
   

    
}
