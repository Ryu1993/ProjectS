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
    private ObjectPool plantObjectPool;
    [SerializeField]
    SceneGem sceneGem;
    [SerializeField]
    SceneCrop sceneCrop;
    [SerializeField]
    ScenePlant scenePlant;
    
    private void Awake()
    {
        gemObjectPool = ObjectPoolManager.Instance.PoolRequest(sceneGem.gameObject, 20,10);
        cropObjectPool = ObjectPoolManager.Instance.PoolRequest(sceneCrop.gameObject, 20, 10);
        plantObjectPool = ObjectPoolManager.Instance.PoolRequest(scenePlant.gameObject, 20, 10);
    }

    public Transform CreateSceneItem(Item item,Vector3 position,ObjectPool objectPool)
    {
        objectPool.Call(position).TryGetComponent(out SceneItem sceneItem);
        sceneItem.ItemSetting(item);
        return sceneItem.transform;
    }

    public Transform CreateSceneItem(Gem gem,Vector3 position)
    {
        return CreateSceneItem(gem,position,gemObjectPool);
    }
    public void CreateSceneItem(Crop crop,Vector3 position)
    {
        Transform temp = cropObjectPool.Call(position);
        temp.GetComponent<SceneCrop>().Crop = crop;
    }
    public void CreateScenePlant(Plant plant, Vector3 position)
    {
        Transform temp = plantObjectPool.Call(position);
        temp.GetComponent<ScenePlant>().Plant = plant;
    }
    public Transform CreateSceneItem(Slime slime, Vector3 position)
    {
        return null;
    }


}
