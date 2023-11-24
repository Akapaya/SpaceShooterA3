using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlayerModel : SpaceShipModel, IDamageble, IShooter
{
    [SerializeField] Transform sourceShoot1;
    [SerializeField] Transform sourceShoot2;

    public delegate void GivePlayerEnergy(int value);
    public static GivePlayerEnergy GivePlayerEnergyHandler;

    public delegate void CastPlayerEnergy(int value);
    public static CastPlayerEnergy CastPlayerEnergyHandler;

    public delegate GameObject GetPlayerGameObject();
    public static GetPlayerGameObject GetPlayerGameObjectHandler;

    private void OnEnable()
    {
        GetPlayerGameObjectHandler += ReturnPlayerGameobject;
        GivePlayerEnergyHandler += TakeEnergy;
        CastPlayerEnergyHandler += CastEnergy;
    }

    private void OnDisable()
    {
        GetPlayerGameObjectHandler -= ReturnPlayerGameobject;
        GivePlayerEnergyHandler -= TakeEnergy;
        CastPlayerEnergyHandler -= CastEnergy;
    }

    public void Start()
    {
        base.Start();
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
        int result = damage;
        if (shipData.Shield > 0)
        {
            result -= shipData.Shield;
            shipData.Shield -= damage;
            OnTakeShield.Invoke(shipData.Shield);
            if (result <= 0)
            {
                return;
            }
        }
        shipData.health -= damage;
        if (shipData.health <= 0)
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            OnDestroy.Invoke();
            GameOverManager.GameOverHandler?.Invoke();
        }
        OnTakeDamage.Invoke(shipData.health);
    }

    public void TakeEnergy(int value)
    {
        shipData.energy += value;
        if (shipData.energy >= shipData.maxEnergy)
        {
            shipData.energy = shipData.maxEnergy;
        }
        OnTakeEnergy.Invoke(shipData.energy);
    }

    public void CastEnergy(int value)
    {
        shipData.energy -= value;
        if (shipData.energy <= 0)
        {
            shipData.energy = 0;
        }
        OnTakeEnergy.Invoke(shipData.energy);
    }

    private GameObject ReturnPlayerGameobject()
    {
        return this.gameObject;
    }
}
