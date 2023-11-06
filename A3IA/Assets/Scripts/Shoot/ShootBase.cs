using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootBase : MonoBehaviour
{
    private Renderer renderer;
    public GameObject sourceShot;
    private SouceTypes sourceType = SouceTypes.Ally;

    public UnityEvent<GameObject> OnHit = new UnityEvent<GameObject>();

    private void Start()
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
            this.gameObject.SetActive(false);
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
}

public enum SouceTypes
{
    Enemy, Ally
}