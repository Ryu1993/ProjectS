using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollCall : MonoBehaviour
{
    [SerializeField]
    GameObject poolTest;
    ObjectPool objectPool;
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    int maxAmount;
    [SerializeField]
    int curAmount;
    private void Awake()
    {
        curAmount = 0;
        maxAmount = 10;
        spawnPosition = poolTest.transform.position;
        objectPool = ObjectPoolManager.Instance.PoolRequest(poolTest,maxAmount,3);
        StartCoroutine(CheckAmount());
    }
    private void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObjects.Add(objectPool.Call(transform.position).gameObject);
        }*/
        if(Input.GetKeyDown(KeyCode.Y))
        {
            foreach(GameObject go  in gameObjects)
            {
                objectPool.Return(go);
                curAmount--;
            }
            gameObjects.Clear();
        }
       

    }
    
    IEnumerator CheckAmount()
    {
        while (true)
       {
            if(curAmount < maxAmount)
            {
                gameObjects.Add(objectPool.Call(spawnPosition, Quaternion.identity).gameObject);
                curAmount++;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
