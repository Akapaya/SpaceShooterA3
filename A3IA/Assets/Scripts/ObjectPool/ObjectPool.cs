using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();

    public delegate GameObject GetShootFromPoolEvent(string tag, Vector3 position, quaternion rotation, GameObject source);
    public static GetShootFromPoolEvent GetShootFromPoolHandle;

    private void OnEnable()
    {
        GetShootFromPoolHandle += SpawnFromPool;
    }

    private void OnDisable()
    {
        GetShootFromPoolHandle -= SpawnFromPool;
    }

    private void Awake()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objectPool);
        }
    }

    public void AddObjectInPool(GameObject fab, int poolSize, string tag)
    {
        Pool newPool = new Pool()
        {
            prefab = fab,
            poolSize = poolSize,
            tag = tag
        };
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < newPool.poolSize; i++)
        {
            GameObject obj = Instantiate(newPool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDict.Add(newPool.tag, objectPool);

        pools.Add(newPool);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, quaternion rotation, GameObject source)
    {
        if (poolDict.ContainsKey(tag))
        {
            GameObject obj = poolDict[tag].Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            poolDict[tag].Enqueue(obj);
            if (tag == "Shoot1" || tag == "Shoot2" || tag == "Shoot3"
                || tag == "Shoot1Enem" || tag == "Shoot2Enem" || tag == "Shoot3Enem")
            {
                obj.GetComponent<ShootBase>().SetSource(source, source.GetComponent<SpaceShipModel>().shipData.souceTypes);
                obj.GetComponent<IShoot>().InitializeShoot();
            }
            return obj;
        }
        return null;
    }
}