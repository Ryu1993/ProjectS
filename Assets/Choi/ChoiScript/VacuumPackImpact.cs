using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VacuumPackImpact : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IItemable target))
        {
            
        }
    }
}
