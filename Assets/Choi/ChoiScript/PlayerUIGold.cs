using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIGold : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private void Awake()
    {
        TryGetComponent(out goldText);
    }

    private void OnEnable()
    {
        PrintGold();
        GameManager.Instance.goldGetEvent += PrintGold;
    }
    private void PrintGold()
    {
        goldText.text = GameManager.Instance.playerGold.ToString() + "G";
    }

    private void OnDisable()
    {
        GameManager.Instance.goldGetEvent -= PrintGold;
    }



}
