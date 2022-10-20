using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateGem : MonoBehaviour
{
    [SerializeField]
    private Gem gem;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ItemManager.Instance.CreateSceneItem(gem, transform.position);
        }
    }

}
