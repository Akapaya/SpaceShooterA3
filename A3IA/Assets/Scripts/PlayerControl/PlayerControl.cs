using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float offSetX = 0;
    private float offSetY = 0.5f;

    void Update()
    {
        Vector3 inputPosition = Vector3.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;
        }
        else if (Input.GetMouseButton(0))
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (inputPosition != Vector3.zero)
        {
            inputPosition.x += offSetX;
            inputPosition.y += offSetY;
            Move(inputPosition);
        }
    }

    private void Move(Vector2 direction)
    {
        transform.position = Vector2.Lerp(transform.position, direction, moveSpeed);
    }
}