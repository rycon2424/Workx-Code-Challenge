using Challenge.Inventory.ScriptableObjects;
using Challenge.Player;
using Challenge.World.Interactables;
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
        [SerializeField] private GameObject worldItemPrefab;

        private InventoryVisualizer inventoryVisualizer;
        private List<InventorySlot> inventorySlotList = new List<InventorySlot>();
        private PlayerBehaviour player;

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(Singleton.gameObject);
            }
            Singleton = this;

            Setup();
        }

        private void Setup()
        {
            player = FindFirstObjectByType<PlayerBehaviour>();

            inventoryVisualizer = GetComponentInChildren<InventoryVisualizer>();
            inventoryVisualizer.InitialSetup();

            OpenCloseInventory(false);
        }

        public void OpenCloseInventory(bool open)
        {
            inventoryVisualizer.CloseSelectionWindow();

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

        public void DropInventoryItem(InventorySlot slot)
        {
            PickUp pickup = Instantiate(worldItemPrefab, player.transform.position, Quaternion.identity).GetComponent<PickUp>();

            pickup.itemInfo = slot.CurrentItemInSlot.ItemInformation;
            pickup.amount = slot.CurrentItemInSlot.CurrentItemCount;

            Destroy(slot.CurrentItemInSlot.gameObject);
        }

        public void SetSlots(List<InventorySlot> generatedSlots)
        {
            inventorySlotList = generatedSlots;
        }

        // Getters/Setters

        public InventoryVisualizer InventoryVisual
        {
            get { return inventoryVisualizer; }
        }
    }
}