using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyCollect : MonoBehaviour
{
    public Recources recources;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Donkey"))
        {
            recources.wood += 11;
            recources.stone += 12;
        }
    }
}
