using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator animator;
    
    private Node current;
    private bool isWalking;

    public Node Current
    {
        get => current;
        set
        {
            StopWalking();

            current = value;
        }
    }
    
    public bool IsWalking
    {
        get => isWalking;
        private set
        {
            isWalking = value;
            if (animator != null)
                animator.SetBool("IsWalking", isWalking);
        }
    }

    private void LateUpdate()
    {
        if (!current || IsWalking)
            return;

        transform.position = current.transform.position;
    }

    public void Walk(List<Node> path)
    {
        StopWalking();

        IsWalking = true;
        WalkCoroutine = StartCoroutine(WalkEnumerator(path, speed));
    }

    private void StopWalking()
    {
        if (IsWalking)
        {
            StopCoroutine(WalkCoroutine);
        }
    }
    
    public Coroutine WalkCoroutine;
    public IEnumerator WalkEnumerator(List<Node> path, float speed)
    {
        float time = 0;
        while (time < path.Count - 1)
        {
            float t = time % 1f;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            
            int nodeIndex = Mathf.FloorToInt(time);
            Node currentNode = path[nodeIndex];
            Node nextNode = path[nodeIndex + 1];
            current = smooth < 0.5f ? currentNode : nextNode;

            transform.position = Vector3.Lerp(currentNode.transform.position, nextNode.transform.position, t);
            
            time += Time.deltaTime * speed;

            yield return null;
        }

        Node endNode = path[path.Count - 1];
        current = endNode;
        transform.position = endNode.transform.position;
        
        IsWalking = false;
    }
}
