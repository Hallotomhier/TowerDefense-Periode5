using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MousePosIndicator : MonoBehaviour
{
    public Grid grid;
    public Material hoverColor;
    private Node hoveredNode;

    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Hit Grid");
            Vector3 worldPosition = hit.point;
            Node closestNode = grid.NodeFromWorldPoint(worldPosition);

            // Change the color of the hovered node here
            if (hoveredNode != null && hoveredNode != closestNode)
            {
                hoveredNode = null; // Reset the previous hovered node
                grid.ResetNodeColors(); // Reset all node colors
            }

            if (closestNode != null && closestNode != hoveredNode)
            {
                hoveredNode = closestNode;
                grid.ResetNodeColors(); // Reset all node colors
                hoveredNode.SetNodeColor(hoverColor); // Change the color of the hovered node
            }
        }
        else
        {
            if (hoveredNode != null)
            {
                hoveredNode = null;
                grid.ResetNodeColors(); // Reset all node colors
            }
        }
    }
}





