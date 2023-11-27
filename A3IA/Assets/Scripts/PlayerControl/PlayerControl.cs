using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float offSetX = 0;
    private float offSetY = 0.5f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 inputPosition = Vector3.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else if (Input.GetMouseButton(0))
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Vector2.Distance(inputPosition, Vector2.zero) > 0.3f)
        {
            inputPosition.x += offSetX;
            inputPosition.y += offSetY;
            Move(inputPosition);
            if (inputPosition.x > 0.1f)
            {
                anim.Play("Right");
            }
            else
            {
                if (inputPosition.x < 0.1f)
                {
                    anim.Play("Left");
                }
            }
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void Move(Vector2 direction)
    {
        transform.position = Vector2.Lerp(transform.position, direction, moveSpeed);
    }
}