using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class EnemyShipModel : SpaceShipModel, IDamageble, IShooter
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
        OnDestroy.AddListener(DestroyWayTraveler);
        shipData.health = shipData.baseHealth;
    }

    private void OnDisable()
    {
        Destroy(waypointsTraveler);
        OnDestroy.RemoveListener(DestroyWayTraveler);
        StopAllCoroutines();
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public void DestroyWayTraveler()
    {
        Destroy(waypointsTraveler);
    }

    public IEnumerator Shoot()
    {
        ObjectPool.GetShootFromPoolHandle(shipData.shootTypes.ToString(), transform.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(Shoot());
    }

    public void TakeDamage(int damage)
    {
        shipData.health--;
        if (shipData.health <= 0)
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            ScoreManager.IncreaseScoreHandler?.Invoke(score);
            OnDestroy.Invoke();
        }
    }
}