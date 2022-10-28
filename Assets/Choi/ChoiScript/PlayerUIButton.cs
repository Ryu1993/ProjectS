using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIButton : MonoBehaviour,ITouchable
{
    [SerializeField]
    private GameObject activateUI;
    private GameObject canvasGO;
    [SerializeField]
    private bool isExit;

    private void Awake()
    {
        canvasGO = transform.GetComponentInParent<Canvas>().gameObject;
    }

    public void ClickEvent()
    {
        if (isExit)
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            activateUI?.SetActive(true);
            canvasGO.SetActive(false);
        }
    }




}
