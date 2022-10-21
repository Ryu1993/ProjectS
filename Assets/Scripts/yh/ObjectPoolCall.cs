using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{
    [SerializeField]
    GameObject slime;
    [SerializeField]
    GameObject plant;
    ObjectPool objectPool;
    ObjectPool plantPool;
    [SerializeField]
    List<GameObject> slimes = new List<GameObject>();
    [SerializeField]
    List<GameObject> plants = new List<GameObject>();
    public Vector3 spawnPosition;
    [SerializeField]
    int maxAmount;
    public int curAmount;

    private void Awake()
    {
        curAmount = 0;
        maxAmount = 50;
        objectPool = ObjectPoolManager.Instance.PoolRequest(slime, maxAmount, 3);
        plantPool = ObjectPoolManager.Instance.PoolRequest(plant, maxAmount, 3);
    }
    private void Start()
    {
        StartCoroutine(CheckSlime());
        StartCoroutine(CheckPlant());
    }

    IEnumerator CheckSlime()
    {

          while (true)
         {
              if (curAmount < maxAmount)
              {
                  yield return new WaitForSeconds(1f);
                  slimes.Add(objectPool.Call(spawnPosition, Quaternion.identity).gameObject);
                  curAmount++;
              }
          }
      }
    IEnumerator CheckPlant()
    {
        while (true)
        {
                yield return new WaitForSeconds(1f);
                plants.Add(plantPool.Call(spawnPosition,Quaternion.Normalize(Quaternion.identity)).gameObject);
        }
    }
}
