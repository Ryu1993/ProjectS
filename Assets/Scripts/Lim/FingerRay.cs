using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRay : MonoBehaviour
{
    public LayerMask mask;
    private UIEventFunc target;

    private void ClickUI()
    {
        if (target != null) // ��ư �Է½÷� ����
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask);//���� �߻� ��ġ ����
            if (hit.transform != null)
            {
                target = hit.transform.GetComponent<UIEventFunc>();
                target?.OnClick.Invoke();
            }
        }
        else if (target == null)//else �� ����
        {
            if (target != null)
            {
                target.OffClick.Invoke();
                target = null;
            }
        }
    }
}
