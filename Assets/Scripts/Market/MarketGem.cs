using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class MarketGem : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI price;
    public float curPrice;
    public Gem curGem;

    public void IconSet()
    {
        iconImage.sprite = curGem ? curGem.itemSprite : iconImage.sprite;
    }
    public void PriceSet()
    {
        curPrice = curGem? (int)Random.Range(curGem.minPrice, curGem.maxPrice) : curPrice;
        price.text = curGem? curPrice.ToString() + "G" : price.text;
    }

}
