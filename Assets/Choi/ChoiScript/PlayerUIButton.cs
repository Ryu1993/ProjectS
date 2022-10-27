using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIButton : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ITouchable target))
        {
            if(isExit)
            {
                Application.Quit();    
            }
            else
            {
                activateUI.SetActive(true);
                canvasGO.SetActive(false);
            }
        }
    }



}
