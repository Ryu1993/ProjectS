using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrabbable : CustomGrabbable
{

    private void Update()
    {
        if(IsGrabed)
        {
            if(Input.GetMouseButton(0))         //�̺κ��� VR ��ǲ�� �°� �����ϸ� ��.
            {
                Debug.Log("�Ѿ� �߻�!!");
            }
            else if(Input.GetMouseButton(1))
            {
                Debug.Log("�Ѿ� ���ε�");
            }
            else
                Debug.Log("���� �����ִ�.");
        }
    }
}
