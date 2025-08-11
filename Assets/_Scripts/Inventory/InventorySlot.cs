using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenge.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [ReadOnly, SerializeField] private ItemSO currentItem;
        [ReadOnly, SerializeField] private int currentItemCount = 0;

        public void OnDrop(PointerEventData eventData)
        {
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (item != null)
            {
                if (ValidatePlacement())
                {
                    SetItemToSlot(item);
                }
            }
        }

        public void SetItem(ItemSO item)
        {
            currentItem = item;
        }

        public void ClearSlot()
        {
            currentItem = null;
            currentItemCount = 0;
        }

        public void SetItemToSlot(InventoryItem item)
        {
            item.transform.SetParent(transform);
            item.transform.SetPositionAndRotation(transform.position, transform.rotation);
            item.transform.localScale = Vector3.one;

            item.SetCurrentSlot(this);
        }

        public bool IsEmpty()
        {
            return currentItem == null;
        }

        private bool ValidatePlacement()
        {
            return true;
        }
    }
}