using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimTest : MonoBehaviour
{
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
        }
    }
}
