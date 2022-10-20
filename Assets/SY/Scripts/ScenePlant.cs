using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ScenePlant : MonoBehaviour, IPoolingable
{
    [SerializeField]
    private Crop crop;
    public Crop Crop { get { return crop; } set { crop = value; } }
    [SerializeField]
    private Plant plant;
    public Plant Plant { get { return plant; }  set { plant = value; } }
    //private float growTime;
    private float bornTime;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private bool isGrow;
    public float growTime;

    public ObjectPool home { get; set; }

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        
    }

    private void OnEnable()
    {
        Debug.Log("dddd");
        StartCoroutine(SettingDelay());
    }

    public void FirstSetting()
    {
        CropManager.Instance.timeChange += TimeChange;
        crop = Plant.Fruit;
        isGrow = false;
        bornTime = 0;
        growTime = Plant.growTime;
    }

    public void TimeChange()
    {
        bornTime++;
        if(isGrow == false)
        {
            isGrow = true;
            meshFilter.mesh = plant.itemMesh;
            meshRenderer.material = plant.itemMaterilal;
        }
        if (bornTime < growTime)
        {
            return;
        }
        else
        {
            MakeFruits();
        }
    }

    public void MakeFruits()
    {
        Debug.Log("뿅");
    }

    IEnumerator SettingDelay()
    {
        yield return new WaitForSeconds(0.3f);
        FirstSetting();
    }
}
