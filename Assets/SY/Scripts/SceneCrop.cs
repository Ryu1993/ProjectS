using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCrop : SceneItem
{
    [SerializeField]
    private Crop crop;
    public Crop Crop { get { return crop; } set { crop = value; } }
    [SerializeField]
    private Plant plant;
    public Plant Plant { get { return plant; } set { plant = value; } }
    private SphereCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<SphereCollider>();
    }
    private void OnEnable()
    {
        
        StartCoroutine(SettingDelay());
    }

    public void Grow()
    {

    }

    IEnumerator SettingDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Plant = Crop.Plant;
        ItemSetting(Crop);
        //meshCollider = ;
    }

}
