using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFX : MonoBehaviour
{
    Animator anim;
    public string animName;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        this.gameObject.SetActive(true);
    }

    public void StopAnim()
    {
        this.gameObject.SetActive(false);
    }
}