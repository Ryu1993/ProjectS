using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FarmMachine : MonoBehaviour
{
    //업그레이드 : 스프링쿨러, 비료, 작물 제거, 비옥한 토양, 기적의 비료
    [SerializeField] LayerMask cropMask;
    public Vector3 machineHeight;
    public float detectRange;
    private bool isPlanted = false;
    [SerializeField]
    private Transform[] plantArea;
    private bool[] isPlanteds;
    private float growTime;
    private float bornTime;
    private bool isWater;

    private void Awake()
    {
        
    }

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
                if (target != null)
                {
                    Plant(target);
                    growTime = target.Plant.growTime;
                    StartGrow();
                    target.ItemReturn();
                    isPlanted = true;
                }
            }
            else
            {
                return;
            }
            
        }
    }

    //물을 오래 안주거나 다른 식물을 심기위한 기능
    public void DeleteCrop()
    {

    }

    public void StartGrow()
    {
        bornTime = 0;
        CropManager.Instance.timeChange += TimeChange;
    }

    public void TimeChange()
    {
        bornTime++;
        if (bornTime < growTime)
        {
            return;
        }
        else
        {
            EndGrow();
        }
    }
    public void EndGrow()
    {
        CropManager.Instance.timeChange -= TimeChange;
        isPlanted = false;
        bornTime = 0;
    }

    public void Plant(SceneCrop crop)
    {
        Transform[] tempArea;
        int randomIndex;
        tempArea = plantArea;

        for (int i = 0; i < crop.Plant.plantCount; i++)
        {
            randomIndex = Random.Range(0, tempArea.Length);
            ItemManager.Instance.CreateScenePlant(crop.Plant, plantArea[randomIndex].position);
        }
        
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + machineHeight, detectRange);
    }

    public void ResetBool()
    {
        //for(int i = 0; i < isPlanted.)
    }
}
