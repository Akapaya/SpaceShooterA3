using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    void Update()
    {
        if(Vector2.Distance(transform.position, endPosition) < 0.5f)
        {
            transform.position = startPosition;
        }
    }
}
