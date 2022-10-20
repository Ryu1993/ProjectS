using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    private float length = 50f;
    [SerializeField]
    private GameObject plantPrefap;
    private ObjectPool spawnPool;
    List<GameObject> spawnList = new List<GameObject> ();



    private void Awake()
    {
        spawnPool = ObjectPoolManager.Instance.PoolRequest(plantPrefap, 10, 0);
    }

    private void Update()
    {
        Vector3 pos = new Vector3(Random.Range(-20f, 20), transform.position.y, Random.Range(80f, 103));
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, transform.forward * length, out hit))
        {
            if (hit.collider.tag == "Terrain")
            {
                spawnList.Add(spawnPool.Call(pos).gameObject);
            }
        }
    }

    private void ReturnPlant()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            spawnPool.Return(spawnList[i]);
        }
    }

    /*
    private void PlantSpawn()
    {
        Vector3 pos = new Vector3(Random.Range(-20f, 20), transform.position.y, Random.Range(80f, 103));
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, transform.forward * length, out hit))
        {
            if(hit.collider.tag == "Terrain")
            {
                spawnList.Add(spawnPool.Call(pos).gameObject);
            }
        }
    }
    */
}
