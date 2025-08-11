using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenge.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [ReadOnly, SerializeField] private GameObject currentItem;

        public void OnDrop(PointerEventData eventData)
        {
            GameObject item = eventData.pointerDrag;
            if (item != null)
            {
                item.transform.SetParent(transform);
                item.transform.SetPositionAndRotation(transform.position, transform.rotation);
            }
        }
    }
}