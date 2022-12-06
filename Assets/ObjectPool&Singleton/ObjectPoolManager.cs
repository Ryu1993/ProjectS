using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

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
        foreach(var pool in m_pools) // ���� ������ Ǯ�߿� �ش� ������Ʈ�� �ش��ϴ� ������ƮǮ�� �ִ��� üũ�� ���� �ִٸ� ������ Ǯ�� ��ȯ
        {
            if(pool.m_baseObj==baseObj)
            {
                return pool;
            }
        }
        //���� ������ Ǯ�߿� ���ٸ� ���Ӱ� ������Ʈ Ǯ�� ������ ��ȯ
        ObjectPool resultPool = new ObjectPool(baseObj, start, add, m_inactive, m_active);
        m_pools.Add(resultPool);
        return resultPool;
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
        foreach (var pool in m_pools) // ���� ������ Ǯ�߿� �ش� ������Ʈ�� �ش��ϴ� ������ƮǮ�� �ִ��� üũ�� ���� �ִٸ� ������Ʈ�� ��ε��Ű�� ���� Ǯ�� ��ȯ
        {
            if (pool.m_baseObj == baseObj)
            {
                Addressables.Release(handle);
                return pool;
            }
        }
        //���� ������ Ǯ�߿� ���ٸ� ���Ӱ� ������Ʈ Ǯ�� ������ ������Ʈ Ǯ�� ���� handle�� ������ ��ȯ
        ObjectPool resultPool = new ObjectPool(baseObj, start, add, m_inactive, m_active);
        resultPool.HandleSet(ref handle);
        m_pools.Add(resultPool);
        return resultPool;
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


}
