using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentratedShot : PowerBase, IShipPower
{
    public ConcentratedShot()
    {
        powerData = new PowerData()
        {
            power = powers.SuperShoot,
        };
    }
}