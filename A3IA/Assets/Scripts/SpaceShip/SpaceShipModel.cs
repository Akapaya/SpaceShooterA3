using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipModel : MonoBehaviour, IDamageble
{
    public SpaceShipData shipData;
    private ObjectPool shootersPool;

    public UnityEvent OnDestroy;

    private void Start()
    {
        shipData = Instantiate(shipData);
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        ObjectPool.GetShootFromPoolHandle("Shoot",transform.position,transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(Shoot());
    }

    public void TakeDamage(int damage)
    {
        shipData.health--;
        if(shipData.health <= 0 )
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            shipData.health = shipData.baseHealth;
            OnDestroy.Invoke();
        }
    }
}