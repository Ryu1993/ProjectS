using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TestInteraction : MonoBehaviour
{
    private enum Mode { Interaction,Release}
    [SerializeField]
    private Image testImage;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;
    [SerializeField]
    private SphereCollider colli;
    [SerializeField]
    private MeshRenderer meshRenderer;
    private Item curItem;
    private Mode curMode = Mode.Interaction;
    private int _itemCount;
    private int itemCount { get { return _itemCount; } set { _itemCount = value; itemCountText.text = value.ToString(); } }
    private UnityAction releasAction;


    private void Update()
    {
        ModeSwitch();
        ReleaseItem();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IItemable item))
        {
            if(curItem !=null)
            {
               if(curItem != item.ItemRequest())
                {
                    return;
                }
            }
            else
            {
                curItem = item.ItemRequest();
                ReleaseActionSet();
                testImage.sprite = curItem.itemSprite;
            }
            itemCount++;
            item.ItemReturn();
        }
    }

    private void ModeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(curMode == Mode.Interaction)
            {
                curMode = Mode.Release;
                colli.enabled = false;
                meshRenderer.material.color = Color.red;
            }
            else
            {
                curMode = Mode.Interaction;
                colli.enabled = true;
                meshRenderer.material.color = Color.green;
            }
        }
    }
    private void ReleaseItem()
    {
        if (itemCount == 0) return;
        if (curMode != Mode.Release) return;
        if(Input.GetKeyDown(KeyCode.R))
        {
            releasAction?.Invoke();
            itemCount--;
            if (itemCount == 0) RemoveSlot();
        }
    }

    private void RemoveSlot()
    {
        testImage.sprite = null;
        curItem = null;
        releasAction = null;
    }

    private void ReleaseActionSet()
    {
        Type curType = curItem.GetType();
        if (curType == typeof(Gem)) releasAction = ReleaseGem;
        if (curType == typeof(Crop)) releasAction = ReleaseCrop;
        if (curType == typeof(Slime)) releasAction = ReleaseSlime;
    }
    private void ReleaseGem()=> ItemManager.Instance.CreateSceneItem(curItem as Gem, transform.position);
    private void ReleaseCrop() => ItemManager.Instance.CreateSceneItem(curItem as Crop, transform.position);
    private void ReleaseSlime() => ItemManager.Instance.CreateSceneItem(curItem as Slime, transform.position);



}
