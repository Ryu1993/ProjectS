using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BC
{
    public class VacuumPack : MonoBehaviour
    {
        [SerializeField]
        Slot[] slots;
        Slot curSlot;
        int slotIndex;
        [SerializeField]
        float releasePower;
        
        [SerializeField]
        private Collider[] vacuumPackColliders;
        public enum TYPE { slime, item }
        private enum MODE { absorption, release }
        public TYPE type = TYPE.slime;
        private MODE mode = MODE.absorption;
        Dictionary<Slot, UnityAction> slotRelease = new Dictionary<Slot, UnityAction>();
   
        private void Update()
        {
            ChangeMode();
            if(Input.GetKeyDown(KeyCode.K))
            {
                SlotChange();
            }
            if (Input.GetKeyUp(KeyCode.L) || Input.GetKeyDown(KeyCode.L))
            {
                if (mode == MODE.release)
                {
                    ItemRelease();
                }
                else if (mode == MODE.absorption)
                {
                    ItemAbsorption();
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
                }
                else if( mode == MODE.absorption)
                {
                    mode = MODE.release;
                }
            }
        }

        private void SlotChange()
        {
            slotIndex++;
            Debug.Log("ÇöÀç½½·Ô: " + slotIndex);
            if(slotIndex == slots.Length)
            {
                slotIndex = 0;
            }
            curSlot = slots[slotIndex];
        }

        private void ItemAbsorption()
        {
            foreach(var col in vacuumPackColliders)
            {
                col.enabled = !col.enabled;
            }
        }

        private void ItemRelease()
        {
            if (curSlot.ItemCount == 0) return;
            curSlot.ItemCount--;
            slotRelease[curSlot]?.Invoke();
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
                        target.ItemReturn();
                        break;
                    }
                }
                foreach(var slot in slots)
                {
                    if(slot.curSlotItem == null)
                    {
                        slot.AddItem(curItem);
                        slotRelease[slot] = ReleaseCheck(curItem);
                        slot.ItemCount++;
                        target.ItemReturn();
                        break;
                    }
                }
            }
        }


        private UnityAction ReleaseCheck(Item item)
        { 
            System.Type itemType = item.GetType();
            if(itemType == System.Type.GetType(itemType.Name))
            {
                return ReleaseGem;
            }
            if(itemType == typeof(Slime))
            {
                return ReleaseSlime;
            }
            if(itemType == typeof(Crop))
            {
                return ReleaseCrop;
            }
            return null;
        }


        private void ReleaseGem()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Gem, transform.position));
        }
        private void ReleaseCrop()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Crop, transform.position));
        }
        private void ReleaseSlime()
        {
            ReleaseForce(ItemManager.Instance.CreateSceneItem(curSlot.curSlotItem as Slime, transform.position));
        }

        private void ReleaseForce(Transform itemTransform)
        {
            itemTransform.TryGetComponent(out Rigidbody rig);
            rig.AddForce(transform.forward * releasePower);
        }
    }
}

