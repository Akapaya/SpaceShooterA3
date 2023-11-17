using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float timeToSpawn = 3;
    Dictionary<int, string> enemyDicts = new Dictionary<int, string>()
    {
        {1,"Enemy1" },
        {2,"Enemy2" },
        {3,"Enemy3" },
        {4,"Boss" },
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
        yield return new WaitForSecondsRealtime(timeToSpawn);
        StartCoroutine(SpawnEnemy());
    }
}