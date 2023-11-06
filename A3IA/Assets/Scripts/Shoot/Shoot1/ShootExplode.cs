using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootExplode : ShootBase, IShoot
{
    #region Components
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Animator animator;
    #endregion

    Vector3 targetPosition = Vector3.zero;
    float radiusExplode = 0.72f;
    bool inMovement = true;
    bool exploded = false;

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
        circleCollider.enabled = false;
        inMovement = true;
        exploded = false;
        circleCollider.radius = radiusExplode;
        GameObject player = ShipPlayerModel.GetPlayerGameObjectHandler();
        targetPosition = new Vector3(transform.position.x, player.transform.position.y, 0);
    }

    void Update()
    {
        if (inMovement)
        {
            transform.Translate((Vector2.up * 3) * Time.deltaTime);
        }
        if(transform.position.y <= targetPosition.y && exploded == false)
        {
            OnHit.Invoke(new GameObject());
        }
    }

    void OnHitEvent(GameObject target)
    {
        if (target.TryGetComponent<IDamageble>(out IDamageble damageble))
        {
            damageble.TakeDamage(1);
        }
        inMovement = false;
        exploded = true;
        Explode();
    }

    void Explode()
    {
        circleCollider.enabled = true;
        animator.Play("Explode");
    }

    public void SetDisable()
    {
        this.gameObject.SetActive(false);
    }
}