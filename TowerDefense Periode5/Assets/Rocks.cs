using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public Grid grid;
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
         
    }
    private void Awake()
    {
        grid = a.transform.GetComponent<Grid>();
    }
    // Update is called once per frame
   /* void Update()
    {
        grid.UpdateNodeWalkability(transform.position, false);
    }
   */
}
