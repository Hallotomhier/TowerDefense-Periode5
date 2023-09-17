using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosIndicator : MonoBehaviour
{
    public class MousePositionIndicator : MonoBehaviour
    {
        public Grid grid;
        public Material hoverMaterial;
        
        private void Start()
        {
            
        }
        private void Update()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
               
                if (hit.transform.CompareTag("Grid"))
                {
                    
                    Vector3 worldPosition = hit.point;
                    Node closestNode = grid.NodeFromWorldPoint(worldPosition);
                    transform.position = closestNode.worldPosition;
                    closestNode.nodeRenderer.material = hoverMaterial;
                }

                
            }
            else
            {
                grid.ResetNodeColors();
            }
        }
    }
}
