using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private Transform[] fruitsTransform;
    private Crop growCrop;
    private float growTime;
    private float bornTime;

    private void OnEnable()
    {
        growTime = growCrop.growTime;
    }

    public void DayChange()
    {
        bornTime++;
        if(bornTime < growTime)
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
        
    }
}
