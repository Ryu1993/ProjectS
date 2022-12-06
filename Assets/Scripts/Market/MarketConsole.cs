using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketConsole : MonoBehaviour
{
    private MarketWindow marketWindow;
    private MarketSlot slot;
    private bool isActive;

    //MarketSlot�� slotAction(MarketSlot�� TriggerEnter�� �߻��� ��� ȹ�� �̺�Ʈ)
    public void Awake()
    {
        slot.slotAction = (Gem gem)=> GameManager.Instance.playerGold += marketWindow.detectionGems[gem].curPrice;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CapsuleCollider player))
        {
            marketWindow.gameObject.SetActive(true);
            marketWindow.MarketPopUp();
            slot.SlotSwitch();
            isActive = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if(isActive)
        {
            slot.SlotSwitch();
            marketWindow.gameObject.SetActive(false);
            isActive = false;
        }
    }
}
