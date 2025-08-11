using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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

        private InventoryManager inventoryManager;

        private List<InventorySlot> generatedEmptySlots = new List<InventorySlot>();

        private void Start()
        {
            inventoryManager = GetComponentInParent<InventoryManager>();

            SetupGrid();
        }

        [Button("Regenerate Grid")]
        private void SetupGrid()
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

            inventoryManager.RetrieveSlots(generatedEmptySlots);
        }
    }
}