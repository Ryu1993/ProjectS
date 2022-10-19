using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCrop : SceneItem
{
    [SerializeField]
    private Crop crop;
    [SerializeField]
    private GameObject plant;
    private float plantTime;

    public void Plant(Transform transform)
    {
        CropManager.Instance.timeChange += Grow;
        gameObject.transform.position = transform.position;
        m_Renderer.enabled = false;
        CropManager.Instance.soilPool.Call(transform.position);
        plantTime = 0;
    }

    public void Grow()
    {
        plantTime++;
        if(plantTime < crop.growTime)
        {
            return;
        }
        else
        {
            Instantiate(plant, gameObject.transform);
        }
    }

    public void MakeFruits()
    {
        Destroy(plant);
        ItemReset();
    }

}
