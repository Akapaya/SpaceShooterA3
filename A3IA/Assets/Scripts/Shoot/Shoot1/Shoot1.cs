using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : ShootBase, IShoot
{
    public void InitializeShoot()
    {
    }

    void Update()
    {
        transform.Translate((Vector2.up * 3) * Time.deltaTime);
    }
}