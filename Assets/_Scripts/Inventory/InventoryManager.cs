using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Singleton;

        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject itemPrefab;

        private List<InventorySlot> inventorySlotList = new List<InventorySlot>();

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(Singleton.gameObject);
            }
            Singleton = this;
        }

        public void OpenCloseInventory(bool open)
        {
            inventory.SetActive(open);
        }

        [Button]
        public bool AddInventoryItem(ItemSO itemToAdd)
        {
            // Check if the item exists with the same SO
            foreach (InventorySlot slot in inventorySlotList)
            {
                if (slot.HasRoom(itemToAdd))
                {
                    slot.IncreaseStackCount();

                    return true;
                }
            }
            
            // Check if there is an empty slot 
            foreach (InventorySlot slot in inventorySlotList)
            {
                if (slot.IsEmpty())
                {
                    InventoryItem item = Instantiate(itemPrefab).GetComponent<InventoryItem>();

                    slot.AddItemToSlot(item);
                    slot.UpdateItemInSlot(item);

                    item.SetupItem(itemToAdd);

                    return true;
                }
            }

            return false;
        }

        public void RemoveInventoryItem(InventorySlot slot)
        {
           Destroy(slot.CurrentItemInSlot.gameObject);
        }

        public void SetSlots(List<InventorySlot> generatedSlots)
        {
            inventorySlotList = generatedSlots;
        }
    }
}