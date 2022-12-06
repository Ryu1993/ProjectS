using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance; // TŸ�� �������� instance;
    public static T Instance // ���� TŸ�� �������� instance�� ���� ������Ƽ
    {
        get 
        {
            if(instance == null)//instance�� ������� ���
            {
                instance = FindObjectOfType(typeof(T)) as T; //���� Ȱ��ȭ�� ������Ʈ�� TŸ�� ������Ʈ�� ���� ������Ʈ�� ã�Ƽ� TŸ������ ����ȯ �� ����.
                if (instance == null) // TŸ�� ������Ʈ�� ���� ������Ʈ�� ��� ������ null������ ���
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();//TŸ�� �̸��� ���� Object�� ����, Object�� TŸ�� ������Ʈ�� �߰��� ����.
                }
            }
            return instance;            
        }
    }

}
