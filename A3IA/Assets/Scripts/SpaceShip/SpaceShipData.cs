using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipData", menuName = "SpaceShip/SpaceShipData", order = 1)]
public class SpaceShipData : ScriptableObject
{
    public string shipName;
    public ShootTypes shootTypes;
    public SouceTypes souceTypes;
    public int health;
    public int baseHealth;
}