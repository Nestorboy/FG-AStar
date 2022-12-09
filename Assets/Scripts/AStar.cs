using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{
    public static List<Node> Processed = new ();

    public static List<Node> GetPath(Node start, Node end)
    {
        List<Node> nodeQueue = new ();
        Processed = new ();

        start.Parent = null;
        nodeQueue.Add(start);
        
        while (nodeQueue.Any())
        {
            Node next = NextNode(nodeQueue);

            next.State = NodeState.Checked;
            // Pop next off of the queue.
            nodeQueue.Remove(next);
            Processed.Add(next);
            
            if (next == end)
            {
                return TracePath(next);
            }

            GatherNeighbors(next, end, nodeQueue, Processed);
        }

        return null; // No possible path.
    }

    private static Node NextNode(List<Node> queue)
    {
        if (!queue.Any())
            return null;

        Node current = queue[0];
        
        foreach (Node node in queue)
        {
            if (node.F < current.F || (node.F == current.F && node.H < current.H))
            {
                current = node;
            }
        }

        return current;
    }

    private static void GatherNeighbors(Node node, Node end, List<Node> queue, List<Node> processed)
    {
        node.Neighbors = node.FindNeighbors();
        foreach (Node neighbor in node.Neighbors)
        {
            if (neighbor.Occupied || processed.Contains(neighbor))
            {
                continue;
            }

            bool inQueue = queue.Contains(neighbor);
            float costToNeighbor = node.G + node.Distance(neighbor);
            
            if (inQueue && costToNeighbor >= neighbor.G)
            {
                continue;
            }
            
            neighbor.G = costToNeighbor;
            neighbor.Parent = node;

            if (inQueue)
                continue;

            neighbor.H = neighbor.Distance(end);
            queue.Add(neighbor);
        }
    }

    private static List<Node> TracePath(Node end)
    {
        List<Node> path = new ();
        Node current = end;
        while (current)
        {
            current.State = NodeState.Path;
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }
}
