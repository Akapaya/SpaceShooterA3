using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        ObjectPool.GetShootFromPoolHandle("Enemy1", transform.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(SpawnEnemy());
    }
}