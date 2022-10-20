using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Crop")]
public class Crop : Item
{
    [SerializeField]
    private float _discardTime;
    public float discardTime { get { return _discardTime; } private set { } }
    [SerializeField]
    private Plant plant;
    public Plant Plant { get { return plant; } private set { } }
}