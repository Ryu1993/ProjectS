using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCount
{
    public Gem gem = null;
    public int count = 0;
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
    private List<GemsCount> gems;

    private void Start()
    {
        StartCoroutine(CheckCoolTime(coolTime));
        gems = new List<GemsCount>(2);
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
                colliders[i].TryGetComponent(out SceneGem target);
                Gem gem = target.ItemRequest() as Gem;
                bool check = CheckGemSlot(gem);
                if(check)
                {
                    target.home.Return(colliders[i].gameObject);
                }
            }
        }
    }

    private bool CheckGemSlot(Gem gem)
    {
        for (int i = 0; i < gems.Count; i++)
        {
            if (gems[i].gem != null)
            {
                if (gems[i].gem == gem)
                {

                }
            }
        }
        if (gems[0].gem == null && gems[1].gem == null)
        {
            gems[0].gem = gem;
            gems[0].count++;
            return true;
        }
        else if (gems[0].gem != null && gems[1].gem == null)
        {
            if (gems[0].gem == gem)
            {
                if (gems[0].count < 100)
                {
                    gems[0].count++;
                    return true;
                }
                else
                {
                    gems[1].gem = gem;
                    gems[1].count++;
                    return true;
                }
            }
        }
        else if (gems[0].gem == null && gems[1].gem != null)
        {
            if (gems[1].gem == gem)
            {
                if (gems[1].count<100)
                {
                    gems[1].count++;
                    return true;
                }
                else
                {
                    gems[0].gem = gem;
                    gems[0].count++;
                    return true;
                }
            }
        }
        else if (gems[0].gem != null && gems[1].gem != null)
        {
            if (gems[0].gem == gem)
            {
                if (gems[0].count<100)
                {
                    gems[0].count++;
                    return true;
                }
                else
                {
                    if (gems[1].gem == gem)
                    {
                        if (gems[1].count<100)
                        {
                            gems[1].count++;
                            return true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else if (gems[1].gem == gem)
            {
                if (gems[1].count < 100)
                {
                    gems[1].count++;
                    return true;
                }
                else
                {
                    if (gems[0].gem == gem)
                    {
                        if (gems[0].count < 100)
                        {
                            gems[0].count++;
                            return true;
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
        return false;
    }

    //상호작용 인터페이스에 저장된 젬 소환기능 추가
    private void SommonGem()
    {

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
