using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CropManager : Singleton<CropManager>
{
    
    [SerializeField]
    private GameObject[] cropPlants;
    public GameObject soil;
    public ObjectPool objectPool;
    public ObjectPool soilPool;
    Dictionary<string, GameObject> cropDic = new Dictionary<string, GameObject>();

    public Action timeChange;

    private void Awake()
    {
        timeChange += Hi;
        //cropDic.Add("Corn", cropPlants[0]);
        //cropDic.Add("Carrot", cropPlants[1]);
        //cropDic.Add("EggPlant", cropPlants[2]);
        //cropDic.Add("Pumpkin", cropPlants[3]);
        //cropDic.Add("Tomato", cropPlants[4]);
    }

    private void Start()
    {
        //objectPool = ObjectPoolManager.Instance.PoolRequest(crops[0], 10, 5);
        //soilPool = ObjectPoolManager.Instance.PoolRequest(soil, 10, 5);
    }

    private void Hi()
    {
        Debug.Log("Hi");
    }
}
