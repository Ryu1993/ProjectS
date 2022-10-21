using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateSlime : MonoBehaviour
{
    [SerializeField]
    private Slime slimeData;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            ItemManager.Instance.CreateSceneItem(slimeData, transform.position);
        }
    }

}
