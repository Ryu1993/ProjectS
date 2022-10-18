using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallFillter : MonoBehaviour
{
    [SerializeField]
    private LayerMask blockLayer;
    private SlimeFarm slimeFarm;
    private float fillterPower = 5f;

    private void OnEnable()
    {
        slimeFarm = GetComponentInParent<SlimeFarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != blockLayer)
            return;

        if(other.transform.parent == slimeFarm.transform)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 opposite = -rb.velocity;
                rb.velocity = Vector3.zero;
                rb.AddForce(opposite * fillterPower, ForceMode.Impulse);
            }
        }
        else
        {
            other.transform.parent = slimeFarm.transform;
        }
    }
}
