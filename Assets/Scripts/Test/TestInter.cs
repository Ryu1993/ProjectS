using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInter : MonoBehaviour
{
    Collider m_collider;
    [SerializeField]
    float speed;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    private void Update()
    {
        Switch();
    }

    private void Switch()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            m_collider.enabled = !m_collider.enabled;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out IInteraction target))
        {
            Vector3 direction = (transform.position - other.transform.position - Physics.gravity).normalized;
            target.rigi.AddForce(direction * speed);
        }
    }





}
