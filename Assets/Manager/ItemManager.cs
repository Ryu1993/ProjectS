using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ItemManager : Singleton<ItemManager>
{
    private ObjectPool gemObjectPool;
    private ObjectPool slimeObjectPool;
    private ObjectPool cropObjectPool;
    [SerializeField]
    SceneGem sceneGem;
    private void Awake()
    {
        gemObjectPool = ObjectPoolManager.Instance.PoolRequest(sceneGem.gameObject, 20, 10);
    }

    public Transform CreateSceneItem(Item item,Vector3 position,ObjectPool objectPool)
    {
        objectPool.Call(position).TryGetComponent(out SceneItem sceneItem);
        sceneItem.ItemSetting(item);
        return sceneGem.transform;
    }

    public Transform CreateSceneItem(Gem gem,Vector3 position)
    {
        return CreateSceneItem(gem,position,gemObjectPool);
    }
    public Transform CreateSceneItem(Crop crop,Vector3 position)
    {
        return CreateSceneItem(crop,position,cropObjectPool);
    }

    public Transform CreateSceneItem(Slime slime, Vector3 position)
    {
        return null;
    }


}
