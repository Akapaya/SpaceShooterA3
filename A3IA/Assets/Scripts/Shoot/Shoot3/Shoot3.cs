using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot3 : ShootBase, IShoot
{
    public void InitializeShoot()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        transform.Translate((Vector2.up * 3) * Time.deltaTime);
    }
}