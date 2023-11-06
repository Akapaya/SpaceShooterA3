using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipModel : MonoBehaviour
{
    public SpaceShipData shipData;

    public UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public UnityEvent<float> OnTakeCure = new UnityEvent<float>();
    public UnityEvent OnDestroy;

    public delegate GameObject GetPlayerGameObject();
    public static GetPlayerGameObject GetPlayerGameObjectHandler;

    private void OnEnable()
    {
        GetPlayerGameObjectHandler += ReturnPlayerGameobject;
    }

    private void OnDisable()
    {
        GetPlayerGameObjectHandler -= ReturnPlayerGameobject;
    }

    private void Start()
    {
        shipData = Instantiate(shipData);
        OnTakeCure.Invoke(shipData.health);
    }

    private GameObject ReturnPlayerGameobject()
    {
        return this.gameObject;
    }
}