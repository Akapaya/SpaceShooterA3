using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlayerModel : SpaceShipModel, IDamageble, IShooter
{
    [SerializeField] Transform sourceShoot1;
    [SerializeField] Transform sourceShoot2;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        ObjectPool.GetShootFromPoolHandle("Shoot1", sourceShoot1.position, transform.rotation, this.gameObject);
        ObjectPool.GetShootFromPoolHandle("Shoot1", sourceShoot2.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Shoot());
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamage.Invoke(shipData.health);
        shipData.health--;
        if (shipData.health <= 0)
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            OnDestroy.Invoke();
        }
    }
}
