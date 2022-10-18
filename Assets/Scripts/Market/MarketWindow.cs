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
    private int curEncounter = 0;
    public Dictionary<Gem, MarketGem> encounterGems = new Dictionary<Gem, MarketGem>();
    private void Awake()=> marketGems = new MarketGem[marketGemsParent.childCount];
    public void MarketPopUp()
    {
        if(GameManager.Instance.encountGems.Count!=0)
        {
            foreach(Gem gem in GameManager.Instance.encountGems)
            {
                encounterGems.Add(gem, marketGems[curEncounter]);
                encounterGems[gem].IconSet();
                encounterGems[gem].PriceSet();
                curEncounter++;
            }
            GameManager.Instance.encountGems.Clear();
        }
        if(curDay!=TimeManager.Instance.dayCount)
        {
            foreach(MarketGem marketGem in encounterGems.Values)
            {
                marketGem.PriceSet();
            }
        }
    }



}
