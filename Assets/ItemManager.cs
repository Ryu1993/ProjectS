using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private GameObject[] crops;
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPoolManager.Instance.PoolRequest(crops[0], 10, 5);
    }
}
