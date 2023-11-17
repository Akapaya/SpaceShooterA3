using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObject : MonoBehaviour
{
    [SerializeField] private float speedY;
    [SerializeField] private float speedX;

    void Update()
    {
        transform.Translate(new Vector2(1* speedX, 1* speedY) * Time.deltaTime);
    }
}
