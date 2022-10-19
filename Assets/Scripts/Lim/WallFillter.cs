using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallFillter : MonoBehaviour
{
    [SerializeField]
    private LayerMask blockLayer;
    private SlimeFarm slimeFarm;

    private void OnEnable()
    {
        slimeFarm = GetComponentInParent<SlimeFarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1<<other.gameObject.layer)&blockLayer)==0)
        {
            return;
        }
        if(other.transform.parent == slimeFarm.transform)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 opposite = Vector3.Reflect(rb.velocity, transform.up);
                rb.velocity = Vector3.zero;
                rb.AddForce(opposite, ForceMode.Impulse);
            }
        }
        else
        {
            other.transform.parent = slimeFarm.transform;
        }
    }
}
