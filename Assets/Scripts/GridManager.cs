using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Node[] nodes;

    private void Start()
    {
        character.Current = RandomNode();
        
        foreach (Node node in nodes)
        {
            node.OnNodeClick += OnNodeClick;
        }
    }

    private void OnNodeClick(Node node, bool isRightClick)
    {
        if (isRightClick)
        {
            if (character.IsWalking)
                return;
            
            node.Occupied = !node.Occupied;
        }
        else
        {
            foreach (Node n in AStar.Processed)
            {
                if (n.Occupied)
                    continue;
                
                n.SetColor(NodeColors.GetStateColor(NodeState.Empty));
            }

            List<Node> path = AStar.GetPath(character.Current, node);
            
            if (path == null)
                return;
            
            character.Walk(path);
        }
    }

    private Node RandomNode()
    {
        int index = Random.Range(0, nodes.Length);
        return nodes[index];
    }
}
