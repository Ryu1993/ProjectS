using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropsCount
{
    public Crop crop;
    public int count;
}

public class AutoFeed : MonoBehaviour
{
    [SerializeField]
    private LayerMask cropsLayer;
    [SerializeField]
    private int feedCount = 3;
    [SerializeField]
    private Transform feedTransform;
    [SerializeField]
    private Transform cropsInputTransform;

    private Crop crop;
    private int count;
    private float coolTime = 5f;
    private bool isCoolTime = false;

    private void Start()
    {
        StartCoroutine(CheckCoolTime(coolTime));
    }

    private void Update()
    {
        AutoFeeding();
        GetCrops();
    }

    private void AutoFeeding()
    {
        if (isCoolTime)
            return;
        for (int i = 0; i < feedCount; i++)
        {
            //������Ʈ Ǯ�� ��ȯ
        }
    }

    private void GetCrops()
    {
        Collider[] colliders = Physics.OverlapSphere(cropsInputTransform.position, 1f, cropsLayer);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //�迭 ã��
                //������ ����
                colliders[i].TryGetComponent(out IPoolingable target);
                target.home.Return(colliders[i].gameObject);
            }
        }
    }
    IEnumerator CheckCoolTime(float value)
    {
        while (true)
        {
            yield return new WaitUntil(() => isCoolTime == false);
            isCoolTime = true;
            yield return new WaitForSeconds(value);
            isCoolTime = false;
        }
    }
}
