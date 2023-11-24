using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootBase : MonoBehaviour
{
    public Renderer renderer;
    public GameObject sourceShot;
    public SouceTypes sourceType = SouceTypes.Ally;

    public UnityEvent<GameObject> OnHit = new UnityEvent<GameObject>();

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetSource(GameObject sourceShot, SouceTypes souceTypes)
    {
        this.sourceShot = sourceShot;
        sourceType = souceTypes;
    }

    private void Update()
    {
        if (renderer.isVisible == false)
        {
            SetDisable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != sourceShot) 
        {
            if(collision.gameObject.GetComponent<SpaceShipModel>().shipData.souceTypes != sourceType)
            {
                OnHit.Invoke(collision.gameObject);
            }
        }
    }

    public void SetDisable()
    {
        this.gameObject.SetActive(false);
    }
}

public enum SouceTypes
{
    Enemy, Ally
}