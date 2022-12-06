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
                    transform.localPosition += new Vector3(0, 0, 1.8f);
                }
                else if (vacuumPack.type == VacuumPack.TYPE.item)
                {
                    vacuumPack.type = VacuumPack.TYPE.slime;
                    transform.localPosition -= new Vector3(0, 0, 1.8f);
                }
            }
        }
    }

}
