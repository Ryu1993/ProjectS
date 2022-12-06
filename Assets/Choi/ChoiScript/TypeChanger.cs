using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC
{
    public class TypeChanger : MonoBehaviour
    {
        [SerializeField]
        VacuumPack vacuumPack;
        Vector3 slimeModePosition = new Vector3(-1.922453f, 1.026342f, 2.587588f);
        Vector3 itemModePosition = new Vector3(-1.922453f, 1.026342f, 0.88f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TestPlayer player)) // �հ����� ��������� �����ؾ���
            {
                if (vacuumPack.type == VacuumPack.TYPE.slime)
                {
                    vacuumPack.type = VacuumPack.TYPE.item;
                    transform.position = itemModePosition;
                    Debug.Log("������ -> ������");
                }
                else if (vacuumPack.type == VacuumPack.TYPE.item)
                {
                    vacuumPack.type = VacuumPack.TYPE.slime;
                    transform.position = slimeModePosition;
                    Debug.Log("������ -> ������ ");
                }
            }
        }
    }

}
