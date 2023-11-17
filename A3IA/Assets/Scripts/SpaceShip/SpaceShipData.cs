using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipData", menuName = "SpaceShip/SpaceShipData", order = 1)]
public class SpaceShipData : ScriptableObject
{
    public string shipName;
    public ShootTypes shootTypes;
    public SouceTypes souceTypes;
    public bool ChangeShoots = false;
    public bool Stunned = false;
    public int MovementSpeed;
    public int health;
    public int baseHealth;
    public int energy;
    public int maxEnergy;
    public int Shield;
    public int durationOfShield = 5;
    public int CadencyOfShoots = 3;
    public int TimeToChangeShoot = 0;
}