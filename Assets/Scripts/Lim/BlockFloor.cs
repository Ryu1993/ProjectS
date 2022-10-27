using BC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Rigidbody rb);
        if (other.TryGetComponent(out SceneSlime sceneSlime))
        {
            sceneSlime.rigi.velocity = Vector3.zero;
            Vector3 centerUp = (transform.position - rb.transform.position).normalized;
            sceneSlime.MoveStop(0.1f);
            rb.AddForce((centerUp + Vector3.up) * 0.3f, ForceMode.Impulse);
        }
    }
}
