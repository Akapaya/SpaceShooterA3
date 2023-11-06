using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<int, string> enemyDicts = new Dictionary<int, string>()
    {
        {1,"Enemy1" },
        {2,"Enemy2" },
        {3,"Enemy3" }
    };

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyDicts.Count);
        string randomValue = enemyDicts.Values.ElementAt(randomIndex);
        ObjectPool.GetShootFromPoolHandle(randomValue, transform.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(SpawnEnemy());
    }
}