using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBase : MonoBehaviour, IShipPower
{
    public SpaceShipModel spaceShipModel;
    public PowerData powerData;

    public void ActivatePower()
    {
        PowerManager.ProcessPowerHandler(powerData, spaceShipModel);
    }
}