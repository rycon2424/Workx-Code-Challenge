using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Challenge.Inventory
{
    public class InventoryVisualizer : MonoBehaviour
    {
        [Header("Inventory Settings")]
        [SerializeField] private int rows = 4;
        [SerializeField] int columns = 4;

        [Header("UI Element Settings")]
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private RectTransform panelSize;
        [SerializeField] private GameObject slotPrefab;

        [Header("UI Item Selection")]
        [SerializeField] private GameObject selectionWindow;
        [SerializeField] private Image selectionBackground;
        [SerializeField] private TMP_Text selectedTitle;
        [SerializeField] private TMP_Text selectedDescription;
        [SerializeField] private Image selectedArt;

        private InventoryItem selectedItem;

        private List<InventorySlot> generatedEmptySlots = new List<InventorySlot>();

        public void InitialSetup()
        {
            CloseSelectionWindow();

            SetupGrid();
        }

        public void CloseSelectionWindow()
        {
            selectionWindow.SetActive(false);
        }

        public void SelectItemInInventory(InventoryItem itemSelected)
        {
            selectionWindow.SetActive(true);

            selectedItem = itemSelected;

            selectedTitle.text = selectedItem.ItemInformation.GetItemName();
            selectedDescription.text = selectedItem.ItemInformation.GetDescription();
            selectedArt.sprite = selectedItem.ItemInformation.GetItemArt();
            selectionBackground.color = selectedItem.ItemInformation.GetItemColor();
        }

        [Button("Regenerate Grid")]
        public void SetupGrid()
        {
            UpdateGridSlots();
            UpdateGridPanel();
        }

        private void UpdateGridPanel()
        {
            float panelWidth = panelSize.rect.width;
            float panelHeight = panelSize.rect.height;

            float totalSpacingX = gridLayout.spacing.x * (columns - 1);
            float totalSpacingY = gridLayout.spacing.y * (rows - 1);

            float availableWidth = panelWidth - totalSpacingX;
            float availableHeight = panelHeight - totalSpacingY;

            float cellWidth = availableWidth / columns;
            float cellHeight = availableHeight / rows;
            float cellSize = Mathf.Min(cellWidth, cellHeight);

            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
            gridLayout.cellSize = new Vector2(cellSize, cellSize);
        }

        private void UpdateGridSlots()
        {
            foreach (InventorySlot slot in generatedEmptySlots)
                Destroy(slot.gameObject);

            generatedEmptySlots.Clear();

            int totalSlots = rows * columns;
            for (int i = 0; i < totalSlots; i++)
            {
                GameObject slot = Instantiate(slotPrefab, gridLayout.transform);
                slot.name = $"Slot {i + 1}";

                generatedEmptySlots.Add(slot.GetComponent<InventorySlot>());
            }

            InventoryManager.Singleton.SetSlots(generatedEmptySlots);
        }

        // Unity Button Events

        // Called/Assigned on a Unity Canvas Button
        public void Button_DropItem()
        {
            selectionWindow.SetActive(false);

            InventoryManager.Singleton.DropInventoryItem(selectedItem.CurrentSlot);
        }
    }
}