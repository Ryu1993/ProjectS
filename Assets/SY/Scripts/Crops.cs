using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour, IPoolingable
{
    public ObjectPool home { get ; set ; }
    [SerializeField]
    private Crop crop;
    [SerializeField]
    private GameObject plant;
    private Renderer renderer;
    private float plantTime;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        

    }
    public void Back()
    {
        home.Return(this.gameObject);
    }

    public void Plant(Transform transform)
    {
        CropManager.Instance.timeChange += Grow;
        gameObject.transform.position = transform.position;
        renderer.enabled = false;
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

    }

}
