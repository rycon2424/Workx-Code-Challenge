using Challenge.Inventory.ScriptableObjects;
using System.Collections.Generic;

namespace Challenge.Inventory
{
    public interface tem
    {
        void AddInventoryItem(ItemSO itemToAdd);
        void RetrieveSlots(List<InventorySlot> generatedSlots);
    }
}