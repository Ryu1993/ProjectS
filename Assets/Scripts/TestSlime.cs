using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlime : MonoBehaviour, IPoolingable
{
    public ObjectPool home { get; set; }

    
    public void Return()
    {
        home.Return(this.gameObject);
    }
   
}
