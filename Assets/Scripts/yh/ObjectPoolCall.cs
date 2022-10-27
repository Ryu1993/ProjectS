using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCall : MonoBehaviour
{

    [SerializeField]
    private int maxPlantAmount;
    private int curPlantAmount;
    WaitForSeconds oneSecond = new WaitForSeconds(1f);
    WaitUntil plantCount;
    [SerializeField]
    private Plant[] plantData;
    [SerializeField]
    LayerMask groundMask;
    Vector3 hitPosition;
    Vector3 randomPosition;
    float range_x;
    float range_z;
    private ScenePlant[] childPlant;

    private void Awake()
    {
        plantCount = new WaitUntil(() => curPlantAmount < maxPlantAmount);
    }
  
    public Vector3 SpawnPosition()
    {
        range_x = Random.Range(transform.position.x, transform.position.x + 10);
        range_z = Random.Range(transform.position.z, transform.position.z + 10);
        randomPosition = new Vector3(range_x, transform.position.y, range_z);
        Ray ray = new Ray(randomPosition, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, groundMask);
        hitPosition = hit.point;
        return hitPosition;
    }
    private IEnumerator Start()
    {
        TimeManager.Instance.halfDayProgressAction += WildeWater;
        while (true)
        {
            yield return plantCount;
            ItemManager.Instance.CreateScenePlant(plantData[Random.Range(0, plantData.Length)], SpawnPosition()).transform.parent = gameObject.transform;
            curPlantAmount++;
            childPlant = GetComponentsInChildren<ScenePlant>();
            yield return oneSecond;
        }
    }

    public void WildeWater()
    {
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                childPlant[i].isWater = true;
            }
        }
    }
    /*private void Temp(BC.SceneSlime sceneSlime)
    {
        Debug.Log("실행 되나?");
        sceneSlime.returnAcition -= WhenDieSlime;
        sceneSlime.returnAcition += WhenDieSlime;

    }*/
}
