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

        private List<GameObject> generatedEmptySlots = new List<GameObject>();

        [Button("Regenerate Grid")]
        private void Start()
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
            foreach (GameObject slot in generatedEmptySlots)
                Destroy(slot);

            int totalSlots = rows * columns;
            for (int i = 0; i < totalSlots; i++)
            {
                GameObject slot = Instantiate(slotPrefab, gridLayout.transform);
                slot.name = $"Slot {i + 1}";
            }
        }
    }
}