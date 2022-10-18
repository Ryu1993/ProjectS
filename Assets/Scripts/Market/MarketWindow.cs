using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MarketWindow : MonoBehaviour
{
    [SerializeField]
    private Transform marketGemsParent;
    [SerializeField]
    private Gem[] encounterGems;
    private MarketGem[] marketGems;
    private int curDay = -1;
    private int curEncounter = 0;


    private void Awake()=> marketGems = new MarketGem[marketGemsParent.childCount];


    public void MarketPopUp()
    {
        if(encounterGems.Length<GameManager.Instance.encounterGems.Count)
        {
            int sub = GameManager.Instance.encounterGems.Count - encounterGems.Length;
            encounterGems = GameManager.Instance.encounterGems.ToArray();
            for(int i =0; i < sub; i++)
            {            
                marketGems[curEncounter].curGem = encounterGems[i];
                marketGems[curEncounter].IconSet();
                marketGems[curEncounter].PriceSet();
                curEncounter++;
            }
        }




    }




}
