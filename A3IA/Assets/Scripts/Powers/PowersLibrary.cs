using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowersLibrary
{
    public static PowerBase ReturnPower(powers power)
    {
        switch (power)
        {
            case powers.Shield:
                {
                    return new Shield();
                }
            case powers.SuperShoot:
                {
                    return new ConcentratedShot();
                }
            case powers.UltraShoot:
                {
                    return new UltimateShoot();
                }
            case powers.None:
            default:
                {
                    return null;
                }
        }
    }
}