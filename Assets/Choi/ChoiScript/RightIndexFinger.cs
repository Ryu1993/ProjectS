using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightIndexFinger : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ITouchable touchable))
        {
            touchable.ClickEvent();
        }
    }

}
