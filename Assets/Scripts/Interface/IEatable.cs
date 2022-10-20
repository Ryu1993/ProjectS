using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEatable : IPoolingable
{
    public Crop CropRequest();
}
