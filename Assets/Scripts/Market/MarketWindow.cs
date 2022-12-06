using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MarketWindow : MonoBehaviour
{
    [SerializeField]
    private Transform marketGemsParent;
    private MarketGem[] marketGems;
    private int curDay = -1;
    private int curDetection = 0;
    public Dictionary<Gem, MarketGem> detectionGems = new Dictionary<Gem, MarketGem>();
    private void Awake()
    {
        marketGems = new MarketGem[marketGemsParent.childCount];
        for(int i =0; i<marketGemsParent.childCount;i++)
        {
            marketGemsParent.GetChild(i).TryGetComponent(out marketGems[i]);
        }
    }


    //MarketPop�� ��� �ű� ȹ���� gem�� ������ �ŷ���Ͽ� �߰�,��¥�� �����ٸ� ���� ������
    public void MarketPopUp()
    {
        if(GameManager.Instance.detectionGems.Count>detectionGems.Count)
        {
            foreach (Gem gem in GameManager.Instance.detectionGems)
            {
                if (detectionGems.TryGetValue(gem, out MarketGem temp))
                {
                    continue;
                }
                detectionGems.Add(gem, marketGems[curDetection]);
                detectionGems[gem].curGem = gem;
                detectionGems[gem].IconSet();
                detectionGems[gem].PriceSet();
                curDetection++;
            }
        }
        if(curDay!=TimeManager.Instance.dayCount)
        {
            foreach(MarketGem marketGem in detectionGems.Values)
            {
                marketGem.PriceSet();
            }
        }
    }



}
