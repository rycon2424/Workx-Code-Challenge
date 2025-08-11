using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.Inventory
{
    public class InventoryManager : MonoBehaviour, tem
    {
        [SerializeField] private GameObject itemPrefab;

        private List<InventorySlot> inventorySlotList = new List<InventorySlot>();

        [Button]
        public void AddInventoryItem(ItemSO itemToAdd)
        {
            foreach (InventorySlot slot in inventorySlotList)
            {
                if (slot.IsEmpty())
                {
                    InventoryItem item = Instantiate(itemPrefab).GetComponent<InventoryItem>();
                    item.SetupItem(itemToAdd);

                    slot.SetItemToSlot(item);
                    slot.SetItem(itemToAdd);

                    break;
                }
            }
        }

        public void RetrieveSlots(List<InventorySlot> generatedSlots)
        {
            inventorySlotList = generatedSlots;
        }
    }
}