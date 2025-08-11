using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.Inventory.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem", order = 1)]
    public class ItemSO : ScriptableObject
    {
        [Header("Item Information")]
        [SerializeField] private int maxStackCount = 1;
        [Space]
        [SerializeField, PreviewField(50)] private Sprite art;
        [SerializeField] private Color backgroundColor = Color.green;

        public Sprite GetItemArt()
        {
            return art;
        }

        public Color GetItemColor()
        {
            return backgroundColor;
        }

        public int GetMaxStackCount()
        {
            return maxStackCount;
        }
    }
}