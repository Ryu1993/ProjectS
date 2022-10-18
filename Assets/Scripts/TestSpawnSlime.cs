using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnSlime : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPrefab;
    private ObjectPool spawnPool;
    List<GameObject> spawnList = new List<GameObject> ();


    private void Awake()
    {
        spawnPool = ObjectPoolManager.Instance.PoolRequest(spawnPrefab, 10, 5);
    }

    private void Update()
    {
        Vector3 pos = new Vector3(Random.Range(72f, 100f), transform.position.y, Random.Range(65f, 100f));
        spawnList.Add(spawnPool.Call(pos).gameObject);
    }

    private void ReturnSlime()
    {
        for(int i = 0; i < spawnList.Count; i++)
        {
            spawnPool.Return(spawnList[i]);
        }
    }
}
