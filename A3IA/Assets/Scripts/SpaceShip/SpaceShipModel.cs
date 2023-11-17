using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipModel : MonoBehaviour
{
    public SpaceShipData shipData;

    public UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public UnityEvent<float> OnTakeEnergy = new UnityEvent<float>();
    public UnityEvent<float> OnTakeShield = new UnityEvent<float>();
    public UnityEvent<float> OnTakeCure = new UnityEvent<float>();
    public UnityEvent OnDestroy;

    public void Start()
    {
        shipData = Instantiate(shipData);
        OnTakeCure.Invoke(shipData.health);
    }

    public void TakeShield(int value)
    {
        shipData.Shield += value;
        OnTakeShield.Invoke(shipData.Shield);
        Invoke("DeactiveShield", shipData.durationOfShield);
    }

    public void DeactiveShield()
    {
        shipData.Shield = 0;
        OnTakeShield.Invoke(shipData.Shield);
    }
}