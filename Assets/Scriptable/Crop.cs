using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Item
{
    [SerializeField]
    private float _growTime;
    public float growTime { get { return _growTime; } private set {} }
    [SerializeField]
    private float _discardTime;
    public float discardTime { get { return _discardTime; } private set {} }
}
