using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlant : MonoBehaviour, IPoolingable
{
    public ObjectPool home { get; set; }

    public void Return()
    {
        home.Return(this.gameObject);
    }
}
