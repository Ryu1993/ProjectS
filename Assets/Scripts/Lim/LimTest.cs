using BC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimTest : MonoBehaviour
{
    //[SerializeField]
    //private Slime crop;
    //Rigidbody rb;
    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Rigidbody rb);
        if(other.TryGetComponent(out SceneSlime sceneSlime))
        {
            Vector3 centerUp = (transform.position-rb.transform.position).normalized;
            sceneSlime.MoveStop(0.1f);
            rb.AddForce((centerUp+Vector3.up)*0.4f, ForceMode.Impulse);
        }
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ItemManager.Instance.CreateSceneItem(crop, transform.position);
        //}
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*0.5f,ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * 0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(Vector3.forward * -0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector3.right * -0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 0.5f, ForceMode.Impulse);
        }
        */
    }

}
