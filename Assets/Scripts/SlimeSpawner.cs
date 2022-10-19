using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    private float timer;
    private float waitingTime = 2f;

    [SerializeField]
    private GameObject spawnSlimePrefab;
    private ObjectPool spawnPool;
    List<GameObject> spawnList = new List<GameObject> ();


    private void Awake()
    {
        spawnPool = ObjectPoolManager.Instance.PoolRequest(spawnSlimePrefab, 10, 0);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            SlimeA();
            timer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawnPool.Return(spawnSlimePrefab);
        }
    }
    private void ReturnSlime()
    {
        for(int i = 0; i < spawnList.Count; i++)
        {
            spawnPool.Return(spawnList[i]);
        }
    }

    private void SlimeA()
    {
        Vector3 pos = new Vector3(Random.Range(72f, 100f), transform.position.y, Random.Range(65f, 100f));
        spawnList.Add(spawnPool.Call(pos).gameObject);
    }
}
