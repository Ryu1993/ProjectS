using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC
{
    public class TypeChanger : MonoBehaviour
    {
        [SerializeField]
        VacuumPack vacuumPack;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TestPlayer player)) // �հ����� ��������� �����ؾ���
            {
                if (vacuumPack.type == VacuumPack.TYPE.slime)
                {
                    vacuumPack.type = VacuumPack.TYPE.item;
                    Debug.Log("������ -> ������");
                }
                else if (vacuumPack.type == VacuumPack.TYPE.item)
                {
                    vacuumPack.type = VacuumPack.TYPE.slime;
                    Debug.Log("������ -> ������ ");
                }
            }
        }
    }

}
