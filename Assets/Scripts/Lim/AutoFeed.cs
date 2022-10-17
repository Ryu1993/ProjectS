using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crops
{
    public Crop crop;
    public int count;
}

public class AutoFeed : MonoBehaviour
{
    private int feedCount = 3;
    private float coolTime = 5f;
    private bool isCoolTime = false;
    private Crops[] crops;

    private void Start()
    {
        StartCoroutine(CheckCoolTime(coolTime));
    }

    private void Update()
    {
        AutoFeeding();
    }

    private void AutoFeeding()
    {
        if (isCoolTime)
            return;
        for (int i = 0; i < feedCount; i++)
        {
            //오브젝트 풀링 소환
        }
    }

    private void GetCrop()
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
