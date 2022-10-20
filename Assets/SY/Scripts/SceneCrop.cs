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
    private MeshCollider meshCollider;

    private void OnEnable()
    {
        meshCollider = GetComponent<MeshCollider>();
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
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = Crop.itemMesh;
    }

}
