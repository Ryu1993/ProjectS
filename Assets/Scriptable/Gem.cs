using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    [SerializeField]
    private float _minPrice;
    public float minPrice { get { return _minPrice; } private set { } }
    [SerializeField]
    private float _maxPrice;
    public float maxPrice { get { return _maxPrice; } private set { } } 

}
