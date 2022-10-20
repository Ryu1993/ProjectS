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
            if (Input.GetKey(KeyCode.Escape))//버튼 나중에 수정
            {
                if (mode == MODE.release)
                {
                    ItemRelease(); // 방출
                }
                else if (mode == MODE.absorption)
                {
                    ItemAbsorption(); //흡수
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

