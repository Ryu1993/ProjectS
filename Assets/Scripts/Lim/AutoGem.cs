using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems
{
    public Gem gem;
    public int count;
}

public class AutoGem : MonoBehaviour
{
    [SerializeField]
    private LayerMask gemLayer;
    private bool isCoolTime = false;
    private Vector3 centerPosition;
    private Vector3 boxSize;
    private Gems[] gems;

    private void HarvestGem()
    {
        if (isCoolTime)
            return;

        Collider[] colliders = Physics.OverlapBox(centerPosition, boxSize, Quaternion.identity, gemLayer);
        if(colliders.Length>0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //�� ����
                //�� ��ȯ����
            }
        }
    }

    //��ȣ�ۿ� �������̽��� ����� �� ��ȯ��� �߰�

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
