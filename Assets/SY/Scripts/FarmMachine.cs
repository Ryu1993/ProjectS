using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class FarmMachine : MonoBehaviour
{
    //업그레이드 : 스프링쿨러, 비료, 작물 제거, 비옥한 토양, 기적의 비료 -> 작물제거는 리스트 제거에서 제외
    [SerializeField] LayerMask cropMask;
    public Vector3 machineHeight;
    public float detectRange;
    private bool isPlanted = false;
    [SerializeField]
    public List<Transform> plantArea = new List<Transform>();
    private float growTime;
    private float bornTime;
    private float rotTime;
    private bool isWater;
    public ScenePlant[] childPlant;
    [SerializeField]
    private GameObject sprinkler;
    [SerializeField]
    private Image cropImage;
    public Crop tempCrop;
    private bool isFertilizer = false;
    private bool isFertileSoil = false;
    private bool isMoreFruits = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SceneCrop crop))
        {
            DetectCrop(crop);
        }
    }

    public void DetectCrop(SceneCrop crop)
    {
        if (!isPlanted)
        {
            Plant(crop);
            cropImage.sprite = crop.Crop.itemSprite;
            growTime = crop.Plant.growTime;
            StartGrow();
            crop.ItemReturn();
            isPlanted = true;
        }
    }

    //작물 탐지
    //public void DetectCrop()
    //{
    //    Collider[] targets = new Collider[1];
    //    /* = Physics.OverlapSphere(transform.position + machineHeight, detectRange, cropMask);*/
    //    Physics.OverlapSphereNonAlloc(transform.position, detectRange, targets,cropMask); //Physics.Overlap~~NonAlloc : 메모리를 매번 할당하지 않고도 사용가능한 메서드       
    //    for (int i = 0; i < targets.Length; i++) //레이어 마스크
    //    {
    //        if(isPlanted == false)
    //        {
    //            SceneCrop target = targets[0].transform.GetComponent<SceneCrop>();
    //            if (target != null)
    //            {
    //                Plant(target);
    //                cropImage.sprite = target.Crop.itemSprite;
    //                growTime = target.Plant.growTime;
    //                StartGrow();
    //                target.ItemReturn();
    //                isPlanted = true;
    //            }
    //        }
    //        else
    //        {
    //            return;
    //        }
            
    //    }
    //}
    //처음 심기 시작했을 때 시간지나는 함수 액션에 추가
    public void StartGrow()
    {
        bornTime = 0;
        TimeManager.Instance.dayProgressAction += TimeChange;
    }

    //작물이 자라는 일정 시간이 지날 때 마다 실행시킬 함수
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
        TimeManager.Instance.dayProgressAction -= TimeChange;
        isPlanted = false;
        bornTime = 0;
        growTime = 0;
    }

    //작물 심는 함수
    public void Plant(SceneCrop crop)
    {
        if (isPlanted == false)
        {
            for (int i = 0; i < plantArea.Count - crop.Plant.plantCount; i++) //개수만큼 식물 심게
            {
                int randomIndex = Random.Range(0, plantArea.Count);
                plantArea.RemoveAt(randomIndex);
            }

            for (int i = 0; i < crop.Plant.plantCount; i++)
            {
                ItemManager.Instance.CreateScenePlant(crop.Plant, plantArea[i].position).parent = gameObject.transform;
            }
            childPlant = GetComponentsInChildren<ScenePlant>();
            isPlanted = true;
            UpgradeCheck();
        }
    }

    //public void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position + machineHeight, detectRange);
    //}

    public void StartFunction(string func)
    {
        Invoke(func, 0.1f);
    }

    //작물 제거 10G
    public void DeleteCrop()
    {
        for(int i = 0; i < childPlant.Length; i++)
        {
            childPlant[i].ItemReturn();
        }
        cropImage.sprite = null;
        childPlant = null;
    }

    //스프링클러(자동으로 물주는 거) : 500G
    public void Sprinkler()
    {
        sprinkler.SetActive(true);
        AutoWater();
        TimeManager.Instance.halfDayProgressAction += AutoWater;
    }

    //기적의 비료(썩는 데 걸리는 시간 추가) : 500G
    public void Fertilizer()
    {
        isFertilizer = true;
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                //childPlant[i].rotTime -= 2f;
            }
        }

    }

    //비옥한 토양(빨리 자라게) : 300G
    public void FertileSoil()
    {
        isFertileSoil = true;
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                //childPlant[i].bornTime += 1f;
            }
        }

    }

    //나오는 열매 개수 증가 : 500G
    public void MoreFruits()
    {
        isMoreFruits = true;
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                //childPlant[i].isMoreFruit = true;
            }
        }
    }

    public void AutoWater()
    {
        sprinkler.transform.FindComponentChild(out ParticleSystem particle);
        sprinkler.transform.GetComponent<AudioSource>().Play();
        particle.Play();
        if (childPlant != null)
        {
            for (int i = 0; i < childPlant.Length; i++)
            {
                childPlant[i].isWater = true;
            }
        }
        else
        {
            return;
        }
    }

    public void UpgradeCheck()
    {
        if (isFertileSoil) FertileSoil();
        if (isFertilizer) Fertilizer();
        if (isMoreFruits) MoreFruits();
    }
}
