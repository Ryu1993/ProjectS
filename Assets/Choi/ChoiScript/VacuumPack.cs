using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BC
{
    public class VacuumPack : MonoBehaviour
    {
        Slot[] slots;
        [SerializeField]
        private MeshRenderer modeCheckRenderer;
        public enum TYPE { slime, item }
        private enum MODE { absorption, release }
        public TYPE type = TYPE.slime;
        private MODE mode = MODE.absorption;



        private void Start()
        {
            modeCheckRenderer.material.color = Color.red;
        }
        private void Update()
        {
            ChangeMode();
            if (Input.GetKey(KeyCode.Escape))//��ư ���߿� ����
            {
                if (mode == MODE.release)
                {
                    ItemRelease(); // ����
                }
                else if (mode == MODE.absorption)
                {
                    ItemAbsorption(); //���
                }
            }
        }

        private void ChangeMode()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                if(mode == MODE.release)
                {
                    mode = MODE.absorption;
                    modeCheckRenderer.material.color = Color.red;
                }
                else if( mode == MODE.absorption)
                {
                    mode = MODE.release;
                    modeCheckRenderer.material.color = Color.green;
                }
            }
        }

        private void ItemAbsorption()
        {

        }

        private void ItemRelease()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IItemable target))
            {
                Item curItem = target.ItemRequest();
                foreach(var slot in slots)
                {
                    if(slot.curSlotItem == curItem)
                    {
                        slot.ItemCount++;
                        break;
                    }
                }
                foreach(var slot in slots)
                {
                    if(slot.curSlotItem == null)
                    {
                        slot.AddItem(curItem);
                        slot.ItemCount++;
                        break;
                    }
                }
            }
        }
    }
}

