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

    private void OnEnable()
    {
        ScoreManager.OnScoreIncreased.AddListener(CheckIfSpawnBoss);
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreIncreased.RemoveListener(CheckIfSpawnBoss);
    }

    private void Start()
    {
        Invoke("CallSpawnEnemyCoroutine", 2f);
    }

    private void CallSpawnEnemyCoroutine()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyDicts.Count - 1);
        string randomValue = enemyDicts.Values.ElementAt(randomIndex);
        ObjectPool.GetShootFromPoolHandle(randomValue, transform.position, transform.rotation, this.gameObject);
        yield return new WaitForSecondsRealtime(timeToSpawn);
        StartCoroutine(SpawnEnemy());
    }

    private void CheckIfSpawnBoss(int value)
    {
        if(value == 1000)
        {
            string randomValue = enemyDicts.Values.ElementAt(3);
            ObjectPool.GetShootFromPoolHandle(randomValue, transform.position, transform.rotation, this.gameObject);
        }
    }
}