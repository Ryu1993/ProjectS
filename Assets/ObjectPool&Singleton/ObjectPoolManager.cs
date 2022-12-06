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
        if(!baseObj.TryGetComponent(out IPoolingable temp)) //����� ������ƮǮ �������̽��� ������ �ִ��� üũ�� ���ٸ� null��ȯ
        {
            return null;
        }
        if (SerachObjectPool(baseObj, out ObjectPool pool)) // ���� ������ Ǯ�߿� �ش� ������Ʈ�� �ش��ϴ� ������ƮǮ�� �ִ��� üũ�� ���� �ִٸ� ������ Ǯ�� ��ȯ
        {
            return pool;
        }
        pool = new ObjectPool(baseObj, start, add, m_inactive, m_active); //���� ������ Ǯ�߿� ���ٸ� ���Ӱ� ������Ʈ Ǯ�� ������ ��ȯ
        m_pools.Add(pool);
        return pool;
    }


    //Addressable�� ObjectPool������ ȣ��
    public ObjectPool PoolRequest(ref AsyncOperationHandle<GameObject> handle, int start, int add)
    {
        GameObject baseObj = handle.WaitForCompletion();//handle�� �޸𸮿� ������Ʈ�� ������ �ε��ų������ ���
        if (!baseObj.TryGetComponent(out IPoolingable temp)) //����� ������ƮǮ �������̽��� ������ �ִ��� üũ�� ���ٸ� ������Ʈ�� ��ε��Ű�� null��ȯ
        {
            Addressables.Release(handle);
            return null;
        }      
        if (SerachObjectPool(baseObj,out ObjectPool pool))// ���� ������ Ǯ�߿� �ش� ������Ʈ�� �ش��ϴ� ������ƮǮ�� �ִ��� üũ�� ���� �ִٸ� ������Ʈ�� ��ε��Ű�� ���� Ǯ�� ��ȯ
        {
            Addressables.Release(handle);
            return pool;
        }
        pool = new ObjectPool(baseObj, start, add, m_inactive, m_active);//���� ������ Ǯ�߿� ���ٸ� ���Ӱ� ������Ʈ Ǯ�� ������ ������Ʈ Ǯ�� ���� handle�� ������ ��ȯ
        pool.HandleSet(ref handle);
        m_pools.Add(pool);
        return pool;
    }

    //Addressable�̿��ؼ� ������Ʈ Ǯ ������ �����ε�
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

    private bool SerachObjectPool(GameObject baseObj,out ObjectPool returnPool) // ���� ������ ������Ʈ Ǯ ����߿��� �˻�
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

    public void RemoveObjectPool(ObjectPool pool) //���� ������ ������Ʈ Ǯ ��Ͽ��� ����
    {
        if(m_pools.Contains(pool))
        {
            m_pools.Remove(pool);
        }
    }



}
