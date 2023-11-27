using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDivide : ShootBase, IShoot
{
    [SerializeField] private Animator animator;

    int numberOfProjects = 5;
    float degree = 40f;
    float timeToDivide = 1.0f;

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
        StartCoroutine(DividedShots());
    }

    void Update()
    {
        transform.Translate((Vector2.up) * Time.deltaTime);
    }

    IEnumerator DividedShots()
    {
        yield return new WaitForSeconds(timeToDivide);
        for (int i = 0; i < numberOfProjects; i++)
        {
            float angle = 60 + (degree * (i + 1));
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            ObjectPool.GetShootFromPoolHandle("Shoot3Enem", transform.position, rotation, sourceShot);
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