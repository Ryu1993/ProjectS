using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class ObjectPool
{
    [SerializeField]
    public GameObject m_baseObj { get; private set; }
    private int m_add;
    private Stack<GameObject> m_pool = new Stack<GameObject>();
    private Transform m_inactive;
    private Transform m_active;
    private AsyncOperationHandle m_baseObjHandle;


    // ObjectPool ������, ������ �Է¹������ ���� �� �ʱⰪ��ŭ Ǯ�� ä���� ObjectPool�� ��ȯ
    public ObjectPool(GameObject baseObj, int start, int add, Transform inactive,Transform active) 
    {
        m_baseObj = baseObj;
        m_add = add;
        m_inactive = inactive;
        m_active = active;
        PoolAdd(start);
    }

    //baseObj�� Addressable�� �ε�������� �޸� ������ ���� AsyncOperationHandle ������ �޼���
    public void HandleSet(ref AsyncOperationHandle<GameObject> handle)
    {
        m_baseObjHandle = handle;
    }

    //ObjectPool�� seed����ŭ baseObj�� ������ ������ ����. ������ ������ Ǯ�� �������̽��� �����ͼ� ������ ������Ʈ Ǯ�� ������ �Է�(�ش� ������Ʈ�� ������ Ǯ�� Return�ϱ� ���� �ʿ�)
    private void PoolAdd(int seed)
    {
        for(int i = 0; i < seed; i++)
        {
            GameObject createdObj = GameObject.Instantiate(m_baseObj, m_inactive);
            createdObj.TryGetComponent(out IPoolingable poolingable);
            poolingable.home = this;
            m_pool.Push(createdObj);
        }
    }

    //ObjectPool�� Return��Ŵ. ������ �Է��� ObjectPool�� ���� ������ ȣ���ؼ� ��ȯ�� �� �ֵ��� public���� ����
    public void Return(GameObject go)
    {
        go.transform.SetParent(m_inactive,false);
        go.transform.position = Vector3.zero;
        m_pool.Push(go);
    }

    //ObjectPool���� ������Ʈ ȣ��� ȣ�� ���� �����ε�
    public Transform Call(Vector3 position,Quaternion rotate,Transform parent,bool worldPositonStay,bool isMove)
    {
        if(m_pool.Count == 0)
        {
            PoolAdd(m_add);
        }
        m_pool.Pop().TryGetComponent(out Transform Objtransform);
        if(isMove)
        {
            Objtransform.position = position;
        }
        Objtransform.rotation = rotate;
        if(parent!=null)
        {
            Objtransform.SetParent(parent, worldPositonStay);
        }
        else
        {
            Objtransform.SetParent(m_active, false);
        }
        return Objtransform;
    }
    public Transform Call(Quaternion rotate) => Call(Vector3.zero, rotate, null, false, false);
    public Transform Call(Transform parent, bool worldPositonStay) => Call(Vector3.zero, Quaternion.identity, parent, worldPositonStay, false);
    public Transform Call(Quaternion rotate, Transform parent, bool worldPositonStay) => Call(Vector3.zero, rotate, parent, worldPositonStay, false);
    public Transform Call(Vector3 position) => Call(position, Quaternion.identity, null, false, true);
    public Transform Call(Vector3 position, Quaternion rotate) => Call(position, rotate, null, false, true);
    public Transform Call(Vector3 position, Transform parent, bool worldPositonStay) => Call(position, Quaternion.identity, parent, worldPositonStay, true);
    public Transform Call(Vector3 position, Quaternion rotate, Transform parent, bool worldPositonStay) => Call(position, rotate, parent, worldPositonStay, true);
    ~ObjectPool()//GC�� �����Ҷ� ���� baseObj�� ��巹����� �޸𸮿� �ε���״ٸ� ���� ������Ű���� ��
    {
        Addressables.Release(m_baseObjHandle);
    }

}
