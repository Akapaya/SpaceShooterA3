using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;
using Random = UnityEngine.Random;

public class EnemyShipModel : SpaceShipModel, IDamageble, IShooter, IStunnable
{
    private int score = 20;
    [SerializeField] private EnemiesTypes enemiesTypes = EnemiesTypes.Base1;
    WaypointsTraveler waypointsTraveler;
    [SerializeField] List<Transform> pointsToShoot = new List<Transform>();

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
        base.Start();
        
        if (shipData.ChangeShoots == true)
        {
            StartCoroutine(ChangeShoot());
        }
        if (enemiesTypes == EnemiesTypes.Blue)
        {
            StartCoroutine(ShootWhenAppear());
        }
        else
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnDisable()
    {
        Destroy(waypointsTraveler);
        OnDestroy.RemoveListener(DestroyWayTraveler);
        StopAllCoroutines();
    }

    public new void Start()
    {
        
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
        ObjectPool.GetShootFromPoolHandle(shipData.shootTypes.ToString(), pointsToShoot[Random.Range(0,pointsToShoot.Count)].position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(shipData.CadencyOfShoots);
        StartCoroutine(Shoot());
    }

    public IEnumerator ShootWhenAppear()
    {
        if(transform.position.y <= 3)
        {
            ObjectPool.GetShootFromPoolHandle(shipData.shootTypes.ToString(), pointsToShoot[Random.Range(0, pointsToShoot.Count)].position, transform.rotation, this.gameObject);
            yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0);
        }
        StartCoroutine(ShootWhenAppear());
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
        StopAllCoroutines();
        yield return new WaitForSecondsRealtime(duration);
        waypointsTraveler.MoveSpeed = shipData.MovementSpeed;
        shipData.Stunned = false;
        StartCoroutine(Shoot());
    }
}