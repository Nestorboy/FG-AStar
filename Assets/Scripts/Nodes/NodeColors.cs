using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeColors
{
    // Foreground
    private static readonly Color32 Magenta = new(255, 69, 135, 255);
    private static readonly Color32 Orange = new(255, 170, 0, 255);
    private static readonly Color32 Cyan = new(0, 255, 159, 255);
    private static readonly Color32 Grey = new(96, 110, 128, 255);
    
    // Background
    private static readonly Color32 Dark = new(37, 46, 58, 255);
    private static readonly Color32 Deep = new(29, 36, 46, 255);
    private static readonly Color32 Abyss = new(21, 24, 33, 255);
    
    public static Color32 GetStateColor(NodeState state)
    {
        switch (state)
        {
            case NodeState.Occupied:
                return Magenta;
            case NodeState.Checked:
                return Orange;
            case NodeState.Path:
                return Cyan;
            default:
                return Deep;
        }
    }
}
