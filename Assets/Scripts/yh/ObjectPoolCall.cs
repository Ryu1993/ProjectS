using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{
    [SerializeField]
    GameObject poolTest;
    ObjectPool objectPool;
    [SerializeField]
    List<GameObject> fields = new List<GameObject>();
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
            //int i = Random.Range(0, 4);
            //spawnPosition = fields[i].transform.position;
            if (curAmount < maxAmount)
            {
                gameObjects.Add(objectPool.Call(spawnPosition, Quaternion.identity).gameObject);
                curAmount++;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
