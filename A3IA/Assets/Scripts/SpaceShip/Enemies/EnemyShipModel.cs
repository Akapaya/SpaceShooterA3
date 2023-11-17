using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;
using Random = UnityEngine.Random;

public class EnemyShipModel : SpaceShipModel, IDamageble, IShooter, IStunnable
{
    private int score = 10;
    EnemiesTypes enemiesTypes = EnemiesTypes.Base1;
    WaypointsTraveler waypointsTraveler;

    void OnEnable()
    {
        this.gameObject.AddComponent<WaypointsTraveler>();
        waypointsTraveler = GetComponent<WaypointsTraveler>();
        waypointsTraveler.AutoStart = true;
        waypointsTraveler.SetWaypointsGroup(EnemiesPathManager.GetPathHandle(enemiesTypes));
        waypointsTraveler.Start();
        waypointsTraveler.MoveSpeed = shipData.MovementSpeed;
        OnDestroy.AddListener(DestroyWayTraveler);
        shipData.health = shipData.baseHealth;
    }

    private void OnDisable()
    {
        Destroy(waypointsTraveler);
        OnDestroy.RemoveListener(DestroyWayTraveler);
        StopAllCoroutines();
    }

    public new void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
        if (shipData.ChangeShoots == true)
        {
            StartCoroutine(ChangeShoot());
        }
    }

    public void DestroyWayTraveler()
    {
        Destroy(waypointsTraveler);
    }

    public IEnumerator ChangeShoot()
    {
        shipData.shootTypes = (ShootTypes)Random.Range(0, Enum.GetValues(typeof(ShootTypes)).Length);
        yield return new WaitForSecondsRealtime(shipData.TimeToChangeShoot);
        StartCoroutine(ChangeShoot());
    }

    public IEnumerator Shoot()
    {
        ObjectPool.GetShootFromPoolHandle(shipData.shootTypes.ToString(), transform.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(shipData.CadencyOfShoots);
        StartCoroutine(Shoot());
    }

    public void TakeDamage(int damage)
    {
        ShipPlayerModel.GivePlayerEnergyHandler(1);
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
            ScoreManager.IncreaseScoreHandler?.Invoke(score);
            ShipPlayerModel.GivePlayerEnergyHandler(5);
            OnDestroy.Invoke();
        }
        OnTakeDamage.Invoke(shipData.health);
    }

    public IEnumerator TakeStun(int duration)
    {
        shipData.Stunned = true;
        waypointsTraveler.MoveSpeed = 0;
        StopCoroutine("Shoot");
        yield return new WaitForSecondsRealtime(duration);
        waypointsTraveler.MoveSpeed = shipData.MovementSpeed;
        shipData.Stunned = false;
        StartCoroutine(Shoot());
    }
}