using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class NodeUtility
{
    public static float Distance(this Node a, Node b) => Vector3.Distance(a.transform.position, b.transform.position);

    public static List<Node> FindNeighbors(this Node node)
    {
        List<Node> neighbors = new ();
        
        for (int i = 0; i < 8; i++) // Allow diagonals
        {
            float radian = i / 4.0f * Mathf.PI;
            Vector3 dir = new Vector3(Mathf.Cos(radian), -Mathf.Sin(radian));
            
            
            var selectable = node.Button.FindSelectableCentered(dir, 0.1f);
            if (selectable)
                neighbors.Add(selectable.GetComponent<Node>());
        }

        return neighbors;
    }

    private static Selectable FindSelectableCentered(this Selectable selectable, Vector3 dir, float bias)
    {
        RectTransform rt = selectable.transform as RectTransform;
        Vector2 size = rt.sizeDelta;
        
        rt.sizeDelta *= bias;
        Selectable bestPick = selectable.FindSelectable(dir);
        rt.sizeDelta = size;

        return bestPick;
    }
}
