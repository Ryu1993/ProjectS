using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject[] UIs;

    private void OnEnable()
    {
        foreach(var ui in UIs)
        {
            ui.gameObject.SetActive(false);
        }
        UIs[0].SetActive(true);
    }

}
