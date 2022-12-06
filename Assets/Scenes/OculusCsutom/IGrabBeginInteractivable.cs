using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomGrabbable
{
    public bool IsGrabed            //���� ���������� üũ�ϴ� ������Ƽ
    {
        get; set; 
    }
    public void GrabBegin(GameObject grabberObj);   //�������,ó���� �κ�
    public void GrabEnd(GameObject grabberObj);     //��������,ó���� �κ�
}
