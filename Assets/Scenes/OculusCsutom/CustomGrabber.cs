using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabber : MonoBehaviour
{
    public enum GRABBER_MODE
    {
        DEFAULT, TOGGLE
    }

    [SerializeField]
    private GameObject currentGrabbableObj;                      //Ʈ���� �浹 �Ͼ ICustomGrabbable�� �ִ� ���ӿ�����Ʈ
    public GRABBER_MODE mode = GRABBER_MODE.DEFAULT;            //��� ���ϱ� (����̸� �ٽ� ������������ �������)
    public KeyCode grabKey = KeyCode.Space;
    private void Update()
    {
        if (currentGrabbableObj == null)
            return;


        switch (mode)
        {

            case GRABBER_MODE.DEFAULT:
                if (Input.GetKeyDown(grabKey))
                {
                    if (currentGrabbableObj.GetComponent<ICustomGrabbable>().IsGrabed == false)      //������� �ƴϸ�,
                        currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabBegin(gameObject); //��� ����
                    else
                        currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabEnd(gameObject);   //������̸�, ���´�.
                }
                break;


            case GRABBER_MODE.TOGGLE:
                if (Input.GetKey(grabKey))
                    currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabBegin(gameObject);     //�������ִ� ���ȸ� ��´�.
                else
                    currentGrabbableObj.GetComponent<ICustomGrabbable>()?.GrabEnd(gameObject);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        ICustomGrabbable tempGrabbable = other.GetComponent<ICustomGrabbable>();    //ICustomGrabbable�� �������� ó��
        Debug.Log(other.gameObject);
        if (tempGrabbable == null) return;

        currentGrabbableObj = other.gameObject;            //ICustomGrabbable�� ������ ����������Ʈ�� �߰�.

    }
    public void OnTriggerExit(Collider other)
    {
        ICustomGrabbable tempGrabbable = other.GetComponent<ICustomGrabbable>();
        if (tempGrabbable == null) return;

        currentGrabbableObj = null;                         //�浹���� ����� ���´�.
    }
}
