using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarketSlot : MonoBehaviour
{
    public UnityAction<Gem> slotAction;
    private SphereCollider slotCollider;

    private void Awake() => slotCollider = GetComponent<SphereCollider>();

    public void SlotSwitch()
    {
        slotCollider.isTrigger = !slotCollider.isTrigger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ISaleable saleObject))
        {
            saleObject.Sale(out Gem gem);
            slotAction?.Invoke(gem);
        }
    }
}
