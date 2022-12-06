using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    public List<Gem> detectionGems = new List<Gem>();
    public float _playerGold;
    public float playerGold
    {
        get { return _playerGold; }
        set 
        { 
            _playerGold = value;
            goldGetEvent?.Invoke();
        }
    }
    public UnityAction goldGetEvent;

    //Gem ȹ��� �̺�Ʈ, �̹� ���� ���̶�� �����ϰ� ó�� ��� ���̶�� detectionGems�� encountGems�� �߰� (market���� ���)
    public void TakeGem(Gem gem)
    {
        if (detectionGems.Contains(gem)) return;
        detectionGems.Add(gem);
    }

    public void TakeItem(Item item)
    {
        Gem tempGem = item as Gem;
        if(tempGem!=null)
        {
            TakeGem(tempGem);
        }
    }


}
