using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenge.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [ReadOnly, SerializeField] private InventoryItem currentItem;

        public void OnDrop(PointerEventData eventData)
        {
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (item != null)
            {
                if (ValidatePlacement(item))
                {
                    item.CleanupCurrentSlot();

                    UpdateItemInSlot(item);
                }
            }
        }

        public void AddItemToSlot(InventoryItem item)
        {
            currentItem = item;

            currentItem.CurrentItemCount = 1;
        }

        public void ClearSlot()
        {
            currentItem = null;
        }

        public void UpdateItemInSlot(InventoryItem item)
        {
            currentItem = item;

            item.transform.SetParent(transform);
            item.transform.SetPositionAndRotation(transform.position, transform.rotation);
            item.transform.localScale = Vector3.one;

            item.CurrentSlot = this;
        }

        public void IncreaseStackCount()
        {
            currentItem.CurrentItemCount++;
        }

        public bool HasRoom(ItemSO itemType)
        {
            if (currentItem != null && currentItem.ItemInformation != null)
            {
                if (currentItem.ItemInformation == itemType && currentItem.CurrentItemCount < itemType.GetMaxStackCount())
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsEmpty()
        {
            return currentItem == null;
        }

        private bool ValidatePlacement(InventoryItem item)
        {
            if (IsEmpty())
            {
                return true;
            }
            if (HasRoom(item.ItemInformation))
            {
                return HandleStacking(item);
            }
            return false;
        }

        private bool HandleStacking(InventoryItem item)
        {
            int maxStack = item.ItemInformation.GetMaxStackCount();
            int spaceLeft = maxStack - currentItem.CurrentItemCount;

            int amountToAdd = Mathf.Min(spaceLeft, item.CurrentItemCount);

            currentItem.CurrentItemCount += amountToAdd;

            item.CurrentItemCount -= amountToAdd;

            if (item.CurrentItemCount <= 0)
            {
                InventoryManager.Singleton.RemoveInventoryItem(item.CurrentSlot);
            }
            else
            {
                return false;
            }

            return true;
        }

        // Get

        public InventoryItem CurrentItemInSlot
        {
            get { return currentItem; }
        }
    }
}