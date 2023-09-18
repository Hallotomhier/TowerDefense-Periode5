using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iheap is voor de queue
public class Node : IHeapItem<Node> 
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    public List<Node> neighbours;
    private int heapIndex;

    public Renderer nodeRenderer;
    //Constructor voor creeren van nieuwe nodes
    public Node(bool _walkable, Vector3 _worldPos,int _gridX,int _gridY)
    {
        // eigenschappen die de node nodig heeft
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        nodeRenderer = null;
        //maakt lijst van neighbour nodes voor huidige node
        neighbours = new List<Node>();
    }
    public void SetNodeColor(Material color)
    {
        if(nodeRenderer != null)
        {
            nodeRenderer.material = color;
        }
        
    }
    // berekent fcost en stuurt dit terug om node te bereiken
    public int fCost 
    {
        get
        {
            return gCost + hCost;
        }
    }
    //zorgt ervoor dat dat node kan worden opgehaald en kan worden returned
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    // vergelijkt node met een andere node doormiddel van fcost en hcost
    public int CompareTo(Node nodeToCompare)
    {
        //vergelijkt fcost van node met de fcost van de compareto fcost node
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        // als fcost gelijk is kijkt het naar de hcost
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        //zorgt ervoor dat laagste fcost altijd eerste in prioriteits queue komt
        return -compare;
    }
}
