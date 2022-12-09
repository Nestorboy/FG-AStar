using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler
{
    private Button _button;
    private Image _image;
    
    private NodeState _state;
    private bool _occupied;
    
    [NonSerialized] public List<Node> Neighbors;
    [NonSerialized] public Node Parent;
    
    public Action<Node, bool> OnNodeClick;
    
    private float _g; // Distance of path length to start;
    private float _h; // Distance from node to end.

    public float G
    {
        get => _g;
        set => _g = value;
    }
    
    public float H
    {
        get => _h;
        set => _h = value;
    }
    
    public float F => G + H; // Add heuristic bias.

    public Button Button => _button;

    public NodeState State
    {
        get => _state;
        set
        {
            _state = value;
            switch (_state)
            {
                case NodeState.Empty:
                case NodeState.Checked:
                case NodeState.Path:
                {
                    if (_occupied) _occupied = false;
                    break;
                }
                case NodeState.Occupied:
                {
                    _occupied = true;
                    break;
                }
            }
            
            SetColor(NodeColors.GetStateColor(_state));
        }
    }

    public bool Occupied
    {
        get => _occupied;
        set
        {
            _occupied = value;
            State = _occupied ? NodeState.Occupied : NodeState.Empty;
        }
    }
    
    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        var t = Neighbors;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnNodeClick?.Invoke(this, false);
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnNodeClick?.Invoke(this, true);
    }

    public void SetColor(Color32 color)
    {
        _image.color = color;
    }
}
