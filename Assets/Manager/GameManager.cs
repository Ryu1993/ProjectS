using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public List<Gem> detectionGems = new List<Gem>();
    public float playerGold;

    //Gem ȹ��� �̺�Ʈ, �̹� ���� ���̶�� �����ϰ� ó�� ��� ���̶�� detectionGems�� encountGems�� �߰� (market���� ���)
    public void TakeGem(Gem gem)
    {
        if (detectionGems.Contains(gem)) return;
        detectionGems.Add(gem);
    }


}
