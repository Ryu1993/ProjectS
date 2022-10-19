using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmMachine : MonoBehaviour
{
    //업그레이드 : 스프링쿨러, 비료, 작물 제거, 비옥한 토양, 기적의 비료
    public Crop crop;
    [SerializeField] LayerMask cropMask;
    public Vector3 machineHeight;
    public float detectRange;
    private bool isPlanted = false;
    [SerializeField]
    private Transform plantArea;
    
    //[SerializeField]
    //private List<Transform> cropTransform = new List<Transform>();

    private void Update()
    {
        DetectCrop();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CropManager.Instance.timeChange();
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    foreach (Transform t in cropTransform)
        //    {
        //        objectPool.Return(t.gameObject);
        //    }
        //    cropTransform.Clear();
        //}
    }

    //
    public void DetectCrop()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position + machineHeight, detectRange, cropMask);
        for (int i = 0; i < targets.Length; i++) //레이어 마스크
        {
            if(isPlanted == false)
            {
                SceneCrop target = targets[0].transform.GetComponent<SceneCrop>();
                target?.Plant(plantArea);
                Debug.Log("심습니다.");
                isPlanted = true;
            }
            else
            {
                Debug.Log("심어져있습니다.");
            }
            
        }
    }

    //물을 오래 안주거나 다른 식물을 심기위한 기능
    public void DeleteCrop()
    {

    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + machineHeight, detectRange);
    }
}
