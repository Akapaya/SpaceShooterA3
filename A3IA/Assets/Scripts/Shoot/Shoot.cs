using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Renderer renderer;
    public GameObject sourceShot;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetSource(GameObject sourceShot)
    {
        this.sourceShot = sourceShot;
    }

    void Update()
    {
        transform.Translate((Vector2.up*3) * Time.deltaTime);
        if(renderer.isVisible == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != sourceShot) 
        {
            if(collision.TryGetComponent<IDamageble>(out IDamageble damageble))
            {
                damageble.TakeDamage(1);
                this.gameObject.SetActive(false);
            }
        }
    }
}