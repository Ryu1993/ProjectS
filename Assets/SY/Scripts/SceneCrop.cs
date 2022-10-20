using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCrop : SceneItem
{
    [SerializeField]
    private Crop crop;
    public Plant plant;

    public void Grow()
    {

    }

    public void MakeFruits()
    {
        ItemReset();
    }

}
