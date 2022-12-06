using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarketConsole : MonoBehaviour
{
    [SerializeField]
    private MarketWindow marketWindow;
    [SerializeField]
    private GameObject nameCanvas;
    [SerializeField]
    private MarketSlot slot;
    private bool isActive;
    private BC.Player player;
    //MarketSlot�� slotAction(MarketSlot�� TriggerEnter�� �߻��� ��� ȹ�� �̺�Ʈ)
    public void Awake()
    {
        transform.FindComponentChild(out slot);
        transform.FindComponentChild(out marketWindow);
        slot.slotAction = (Gem gem)=> GameManager.Instance.playerGold += marketWindow.detectionGems[gem].curPrice;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out player))
        {
            nameCanvas.SetActive(!nameCanvas.activeSelf);
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
            if (other.TryGetComponent(out player))
            {
                nameCanvas.SetActive(!nameCanvas.activeSelf);
                slot.SlotSwitch();
                marketWindow.gameObject.SetActive(false);
                isActive = false;
            }
        }
    }


}
