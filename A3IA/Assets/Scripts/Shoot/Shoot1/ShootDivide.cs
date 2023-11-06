using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDivide : ShootBase, IShoot
{
    [SerializeField] private Animator animator;

    int numberOfProjects = 5;
    float degree = 40f;

    public void InitializeShoot()
    {
        animator.Play("NoneShootAnim");
        Invoke("Divided", 5f);
    }

    void Update()
    {
        transform.Translate((Vector2.up * 3) * Time.deltaTime);
    }
   
    void Divided()
    {
        for (int i = 0; i < numberOfProjects; i++)
        {
            float angle = 60 + (degree * (i + 1));
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            ObjectPool.GetShootFromPoolHandle("Shoot1", transform.position, rotation, sourceShot);
        }
        this.gameObject.SetActive(false);
    }
}