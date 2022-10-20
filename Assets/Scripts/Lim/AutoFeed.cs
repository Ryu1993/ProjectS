using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private SlimeFarm slimeFarm;
    private Crop crop;
    private int count = 0;
    private float coolTime = 5f;
    private bool isCoolTime = false;

    private void Start()
    {
        slimeFarm = GetComponentInParent<SlimeFarm>();
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
            if (count <= 0)
            {
                crop = null;
                break;
            }
            Transform cropTranform = ItemManager.Instance.CreateSceneItem(crop, feedTransform.position);
            Rigidbody cropRigidbody = cropTranform.GetComponent<Rigidbody>();
            cropTranform.parent = slimeFarm.InsideObject.transform;
            cropRigidbody.AddForce(cropTranform.forward);
            count--;
        }
    }

    private void GetCrops()
    {
        Collider[] colliders = Physics.OverlapSphere(cropsInputTransform.position, 1f, cropsLayer);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Crop targetCrop = colliders[i].GetComponent<Crop>();
                if(crop == null)
                {
                    crop = targetCrop;
                }
                if (crop != targetCrop)
                    return;
                count++;
                colliders[i].TryGetComponent(out IPoolingable target);
                target.home.Return(colliders[i].gameObject);
            }
        }
    }

    //진공팩 상호작용(빨아들일때) 저장된 먹이 배출
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
