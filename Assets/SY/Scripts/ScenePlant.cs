using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Filtering;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

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
        StartCoroutine(SettingDelay());
    }

    public void FirstSetting()
    {
        CropManager.Instance.timeChange += TimeChange;
        Crop = Plant.Fruit;
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
        //열매 만들때 Crop Plant objectScript넣어주기
        ItemManager.Instance.CreateSceneItem(crop, transform.position);
        ItemReturn();
    }

    IEnumerator SettingDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FirstSetting();
    }

    public void ItemReturn()
    {
        ItemReset();
        home.Return(this.gameObject);
    }

    protected void ItemReset()
    {
        //curItem = null;
        meshFilter.mesh = null;
        meshRenderer.material = null;
    }

}
