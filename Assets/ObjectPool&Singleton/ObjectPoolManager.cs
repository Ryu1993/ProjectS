using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField]
    private List<ObjectPool> m_pools = new List<ObjectPool>();
    private Transform m_active;
    private Transform m_inactive;
    private void Awake()
    {
        m_active = new GameObject("Active").transform;
        m_inactive = new GameObject("InActive").transform;
        m_inactive.gameObject.SetActive(false);
    }

    public ObjectPool PoolRequest(GameObject baseObj,int start,int add)
    {
        if(!baseObj.TryGetComponent(out IPoolingable temp)) //대상이 오브젝트풀 인터페이스를 가지고 있는지 체크후 없다면 null반환
        {
            return null;
        }
        if (SerachObjectPool(baseObj, out ObjectPool pool)) // 현재 생성된 풀중에 해당 오브젝트에 해당하는 오브젝트풀이 있는지 체크후 만약 있다면 기존의 풀을 반환
        {
            return pool;
        }
        pool = new ObjectPool(baseObj, start, add, m_inactive, m_active); //현재 생성된 풀중에 없다면 새롭게 오브젝트 풀을 생성후 반환
        m_pools.Add(pool);
        return pool;
    }


    //Addressable로 ObjectPool생성시 호출
    public ObjectPool PoolRequest(ref AsyncOperationHandle<GameObject> handle, int start, int add)
    {
        GameObject baseObj = handle.WaitForCompletion();//handle이 메모리에 오브젝트를 완전히 로드시킬때까지 대기
        if (!baseObj.TryGetComponent(out IPoolingable temp)) //대상이 오브젝트풀 인터페이스를 가지고 있는지 체크후 없다면 오브젝트를 언로드시키고 null반환
        {
            Addressables.Release(handle);
            return null;
        }      
        if (SerachObjectPool(baseObj,out ObjectPool pool))// 현재 생성된 풀중에 해당 오브젝트에 해당하는 오브젝트풀이 있는지 체크후 만약 있다면 오브젝트를 언로드시키고 기존 풀을 반환
        {
            Addressables.Release(handle);
            return pool;
        }
        pool = new ObjectPool(baseObj, start, add, m_inactive, m_active);//현재 생성된 풀중에 없다면 새롭게 오브젝트 풀을 생성후 오브젝트 풀에 현재 handle을 세팅후 반환
        pool.HandleSet(ref handle);
        m_pools.Add(pool);
        return pool;
    }

    //Addressable이용해서 오브젝트 풀 생성시 오버로딩
    public ObjectPool PoolRequest(IResourceLocation location, int start, int add)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(location);
        return PoolRequest(ref handle, start, add);
    }
    public ObjectPool PoolRequest(AssetReference reference, int start, int add)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(reference);
        return PoolRequest(ref handle, start, add);
    }

    private bool SerachObjectPool(GameObject baseObj,out ObjectPool returnPool) // 현재 생성된 오브젝트 풀 목록중에서 검색
    {
        foreach(var pool in m_pools)
        {
            if (pool.m_baseObj == baseObj)
            {
                returnPool = pool;
                return true;
            }
        }
        returnPool = null;
        return false;
    }

    public void RemoveObjectPool(ObjectPool pool) //현재 생성된 오브젝트 풀 목록에서 삭제
    {
        if(m_pools.Contains(pool))
        {
            m_pools.Remove(pool);
        }
    }



}
