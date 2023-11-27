using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

public class PowerManager : MonoBehaviour
{
    [SerializeField] private ObjectPool gamePool;
    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> shootsList = new List<GameObject>();

    public VisualFX commonFx;
    public VisualFX superFx;
    public VisualFX ultraFx;

    public delegate void ProcessPowerEvent(PowerData powerData, SpaceShipModel spaceShipModel);
    public static ProcessPowerEvent ProcessPowerHandler;

    private void OnEnable()
    {
        ProcessPowerHandler += ProcessPower;
    }

    private void OnDisable()
    {
        ProcessPowerHandler -= ProcessPower;
    }

    private void Start()
    {
        if (gamePool.poolDict.ContainsKey("Enemy1"))
        {
            Queue<GameObject> enemiesQueue = gamePool.poolDict["Enemy1"];
            enemiesList.AddRange(enemiesQueue);
        }

        if (gamePool.poolDict.ContainsKey("Enemy2"))
        {
            Queue<GameObject> enemiesQueue = gamePool.poolDict["Enemy2"];
            enemiesList.AddRange(enemiesQueue);
        }

        if (gamePool.poolDict.ContainsKey("Enemy3"))
        {
            Queue<GameObject> enemiesQueue = gamePool.poolDict["Enemy3"];
            enemiesList.AddRange(enemiesQueue);
        }

        if (gamePool.poolDict.ContainsKey("Shoot1"))
        {
            Queue<GameObject> shootsQueue = gamePool.poolDict["Shoot1"];
            shootsList.AddRange(shootsQueue);
        }

        if (gamePool.poolDict.ContainsKey("Shoot1Enem"))
        {
            Queue<GameObject> shootsQueue = gamePool.poolDict["Shoot1Enem"];
            shootsList.AddRange(shootsQueue);
        }

        if (gamePool.poolDict.ContainsKey("Shoot2Enem"))
        {
            Queue<GameObject> shootsQueue = gamePool.poolDict["Shoot2Enem"];
            shootsList.AddRange(shootsQueue);
        }
    }

    public void ProcessPower(PowerData powerData, SpaceShipModel spaceShipModel)
    {
        switch (powerData.power)
        {
            case powers.None:
                {
                    break;
                }
            case powers.Shield:
                {
                    commonFx.PlayAnim();
                    spaceShipModel.TakeShield(powerData.data["Shield"]);
                    break;
                }
            case powers.SuperShoot:
                {
                    ProcessSuperShootPower();
                    break;
                }
            case powers.UltraShoot:
                {
                    ProcessUltraShootPower(powerData);
                    break;
                }
        }
    }

    public void ProcessSuperShootPower()
    {
        superFx.PlayAnim();
        foreach (GameObject shoots in shootsList)
        {
            ShootBase shootBase = shoots.GetComponent<ShootBase>();
            if (shootBase.sourceType == SouceTypes.Enemy)
            {
                shoots.SetActive(false);
            }
        }
    }

    public void ProcessUltraShootPower(PowerData powerData)
    {
        ultraFx.PlayAnim();
        foreach (GameObject enemy in enemiesList)
        {
            EnemyShipModel enemyShip = enemy.GetComponent<EnemyShipModel>();
            enemyShip.TakeDamage(powerData.data["Damage"]);
            StartCoroutine(enemyShip.TakeStun(powerData.data["StunTime"]));
        }
    }
}