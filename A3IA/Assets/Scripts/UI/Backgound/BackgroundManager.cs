using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private ObjectPool objectPool;
    [SerializeField] private List<Vector2> positionsToSpawn;
    
    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
        StartCoroutine(SpawnObject());
    }

    public IEnumerator SpawnObject()
    {
        yield return new WaitForSecondsRealtime(10f);
        int random = Random.Range(0, objectPool.pools.Count);
        string tag = objectPool.pools[random].tag;
        GameObject instantiated = objectPool.SpawnFromPool(tag, positionsToSpawn[Random.Range(0, positionsToSpawn.Count)], Quaternion.identity, this.gameObject);
        StartCoroutine(SpawnObject());
    }
}
