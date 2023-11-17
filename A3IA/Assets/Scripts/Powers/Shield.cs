using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerBase, IShipPower
{
    public Shield()
    {
        powerData = new PowerData()
        {
            power = powers.Shield,
            data = new Dictionary<string, int>()
            {
                {"Shield",100 },
            },
        };
    }
}