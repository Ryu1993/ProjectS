using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour, IPoolingable
{
    public ObjectPool home { get ; set ; }
    [SerializeField]
    private Crop crop;

    private void OnEnable()
    {
        //Invoke("Back", 1f);
    }

    public void Back()
    {
        home.Return(this.gameObject);
    }

}
