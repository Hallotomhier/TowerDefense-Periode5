using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHoverHandler : MonoBehaviour
{
    public Grid grid;
    public Node node;
    private Renderer nodeRenderer;
    private Material originalColor;
    public GameObject buildingCam;

    private void Start()
    {
        nodeRenderer = GetComponent<Renderer>();
        originalColor = nodeRenderer.material;
    }

    public void OnMouseEnter()
    {
        if (buildingCam == true)
        {
            if (node != null)
            {
                nodeRenderer.material = grid.hoverColor;
            }
        }
       
    }

    private void OnMouseExit()
    {

        if (node != null)
        {
            nodeRenderer.material = originalColor;
        }
    }
}
