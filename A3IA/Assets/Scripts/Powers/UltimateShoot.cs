using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateShoot : PowerBase, IShipPower
{
    public UltimateShoot()
    {
        powerData = new PowerData()
        {
            power = powers.UltraShoot,
            data = new Dictionary<string, int>()
            {
                {"StunTime",3 },
                {"Damage",1 },
            },
        };
    }
}