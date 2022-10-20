using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPooling : MonoBehaviour, IPoolingable
{
    public ObjectPool home { get; set; }
    public ObjectPoolCall objectPoolCall;
    private void OnDisable()
    {
        objectPoolCall.curAmount--;
    }
}
