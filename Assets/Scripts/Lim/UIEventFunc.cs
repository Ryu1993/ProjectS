using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventFunc : MonoBehaviour
{
    private UIManager uimanager;
    public UnityEvent OnClick;
    public UnityEvent OffClick;
    public Upgrade upgrade;
    public Farm farm;
    public GameObject machine;
    private int upgradePrice;
    private bool isClick = false;

    private void Awake()
    {
        uimanager = GetComponentInParent<UIManager>();
    }

    public void ActiveFalse(GameObject target)
    {
        target.SetActive(false);
    }
    public void ActiveTrue(GameObject target)
    {
        target.SetActive(true);
    }
    public void ChangeValue(Scrollbar target)
    {
        if (isClick)
        {
            target.value += Input.GetAxis("Vertical") * 0.5f;
        }
    }
    public void ShowInfo()
    {
        uimanager.Instance.MachineUI.ShowInfo(upgrade);
    }

    public void BuyUpgrade()
    {
        uimanager.Instance.MachineController.BuyUpgrade();
    }

    public void ChangeBool(bool value)
    {
        isClick = value;
    }

    public void SelectFarmType()
    {
        Debug.Log("selectfarm");
        uimanager.Instance.MachineController.farm = farm;
        uimanager.Instance.MachineUI.farm = farm;
        uimanager.Instance.MachineController.SelectFarm();
        machine.SetActive(true);
    }
}
