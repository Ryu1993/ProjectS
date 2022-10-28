using BC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SceneSlime sceneSlime))
        {
            sceneSlime.rigi.velocity = Vector3.zero;
            Vector3 centerUp = (transform.position - sceneSlime.transform.position).normalized;
            sceneSlime.rigi.AddForce((centerUp + Vector3.up) * 0.3f, ForceMode.Impulse);
        }
    }
}
