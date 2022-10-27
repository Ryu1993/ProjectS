using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRay : MonoBehaviour
{
    public LayerMask mask;
    private UIEventFunc target;
    private RaycastHit hit;
    private Vector3[] vecs = new Vector3[2];
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Transform rayPoint;

  

    private void Update()
    {
        ClickUI();
    }

    private void ClickUI()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            line.gameObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            line.gameObject.SetActive(false);
        }
        if (OVRInput.Get(OVRInput.Button.Three)) // 버튼 입력시로 변경
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 9f, mask,QueryTriggerInteraction.Collide))
            {
                vecs[0] = transform.position;
                vecs[1] = hit.point;
                line.SetPositions(vecs);
                if (target == null)
                {
                    hit.transform.TryGetComponent(out target);
                    target?.OnClick.Invoke();
                }
            }
            else
            {
                rayPoint.localPosition = new Vector3(0, 0, -90f);
                vecs[0] = transform.position;
                vecs[1] = rayPoint.position;
                line.SetPositions(vecs);
            }
            //레이 발사 위치 조정
        }
        else
        {
            if (target != null)
            {
                target?.OffClick.Invoke();
                target = null;
            }
        }
        

    }
}
