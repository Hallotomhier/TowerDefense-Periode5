using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHoverHandler : MonoBehaviour
{
    public Grid grid;
    public Node node;
    private Renderer nodeRenderer;
    private Color originalColor;

    private void Start()
    {
        nodeRenderer = GetComponent<Renderer>();
        originalColor = nodeRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (node != null)
        {
            nodeRenderer.material.color = grid.hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (node != null)
        {
            nodeRenderer.material.color = originalColor;
        }
    }
}
