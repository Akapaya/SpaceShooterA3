using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : ShootBase, IShoot
{
    public void InitializeShoot()
    {
    }

    private void OnEnable()
    {
        OnHit.AddListener(OnHitEvent);
    }

    private void OnDisable()
    {
        OnHit.RemoveListener(OnHitEvent);
    }

    void Update()
    {
        transform.Translate((Vector2.up * 3) * Time.deltaTime);
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