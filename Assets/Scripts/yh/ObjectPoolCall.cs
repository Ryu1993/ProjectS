using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{
    [SerializeField]
    GameObject slime;
    ObjectPool objectPool;
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    public Vector3 spawnPosition;
    [SerializeField]
    int maxAmount;
    public int curAmount;

    private void Awake()
    {
        curAmount = 0;
        maxAmount = 50;
        objectPool = ObjectPoolManager.Instance.PoolRequest(slime, maxAmount, 3);
    }
    private void Start()
    {
        StartCoroutine(CheckAmount());
    }

    IEnumerator CheckAmount()
    {

        while (true)
       {
            if (curAmount < maxAmount)
            {
                yield return new WaitForSeconds(1f);
                gameObjects.Add(objectPool.Call(spawnPosition, Quaternion.identity).gameObject);
                curAmount++;
            }
        }
    }

}
