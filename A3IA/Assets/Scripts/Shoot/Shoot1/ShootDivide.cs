using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDivide : ShootBase, IShoot
{
    [SerializeField] private Animator animator;

    int numberOfProjects = 5;
    float degree = 40f;
    float timeToDivide = 0.8f;

    private void OnEnable()
    {
        OnHit.AddListener(OnHitEvent);
    }

    private void OnDisable()
    {
        OnHit.RemoveListener(OnHitEvent);
    }

    public void InitializeShoot()
    {
        animator.Play("NoneShootAnim");
        Invoke("Divided", timeToDivide);
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

    void OnHitEvent(GameObject target)
    {
        if (target.TryGetComponent<IDamageble>(out IDamageble damageble))
        {
            damageble.TakeDamage(1);
            SetDisable();
        }
    }
}