using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCount
{
    public Gem gem;
    public int count;
}

public class AutoGem : MonoBehaviour
{
    [SerializeField]
    private LayerMask gemLayer;
    [SerializeField]
    private Transform centerTransform;
    [SerializeField]
    private Vector3 boxSize;
    private float coolTime = 5f;
    private bool isCoolTime = false;
    private GemsCount[] gems;

    private void Start()
    {
        StartCoroutine(CheckCoolTime(coolTime));
    }
    private void Update()
    {
        HarvestGem();
    }

    private void HarvestGem()
    {
        if (isCoolTime)
            return;

        Collider[] colliders = Physics.OverlapBox(centerTransform.position, boxSize, Quaternion.identity, gemLayer);
        if(colliders.Length>0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //배열 찾기
                //젬 저장
                colliders[i].TryGetComponent(out IPoolingable target);
                target.home.Return(colliders[i].gameObject);
            }
        }
    }

    //상호작용 인터페이스에 저장된 젬 소환기능 추가

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
