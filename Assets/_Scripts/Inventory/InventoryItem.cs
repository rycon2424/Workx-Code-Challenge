using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Challenge.Inventory
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Item Info")]
        [SerializeField, ReadOnly] private ItemSO itemInformation;
        [Header("Item Art")]
        [SerializeField] private Image itemArt;
        [SerializeField] private Image itemBackground;

        private InventorySlot currentSlot;
        private Transform originalParent;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetupItem(ItemSO itemInfo)
        {
            itemArt.sprite = itemInfo.GetItemArt();
            itemBackground.color = itemInfo.GetItemColor();
        }

        public void SetCurrentSlot(InventorySlot slot)
        {
            currentSlot = slot;
        }

        // Events

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalParent = transform.parent;
            transform.SetParent(canvas.transform);

            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;

            // If not dropped on valid slot return to original
            if (transform.parent == canvas.transform)
            {
                transform.SetParent(originalParent);
                transform.SetPositionAndRotation(originalParent.position, originalParent.rotation);
            }
        }
    }
}