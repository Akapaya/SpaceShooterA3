using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class EnemyBase : SpaceShipModel
{
    EnemiesTypes enemiesTypes = EnemiesTypes.Base1;
    WaypointsTraveler waypointsTraveler;

    void OnEnable()
    {
        this.gameObject.AddComponent<WaypointsTraveler>();
        waypointsTraveler = GetComponent<WaypointsTraveler>();
        waypointsTraveler.AutoStart = true;
        waypointsTraveler.SetWaypointsGroup(EnemiesPathManager.GetPathHandle(enemiesTypes));
        waypointsTraveler.Start();
        StartCoroutine(Shoot());
        OnDestroy.AddListener(DestroyWayTraveler);
    }

    private void OnDisable()
    {
        Destroy(waypointsTraveler);
        OnDestroy.RemoveListener(DestroyWayTraveler);
    }

    public void DestroyWayTraveler()
    {
        Destroy(waypointsTraveler);
    }
}