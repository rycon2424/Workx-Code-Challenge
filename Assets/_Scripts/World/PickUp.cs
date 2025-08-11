using Challenge.Inventory;
using Challenge.Inventory.ScriptableObjects;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Challenge.World.Interactables
{
    public class PickUp : MonoBehaviour, IPickUpAble
    {
        [Header("Item")]
        public ItemSO itemInfo;
        public int amount = 1;

        [Header("References / Settings")]
        [SerializeField] private float rotateSpeed = 10;
        [SerializeField] private SpriteRenderer itemSprite;
        [SerializeField] private TMP_Text amountText;

        private void Start()
        {
            Setup();
        }

        [Button("Reload")]
        private void Setup()
        {
            itemSprite.sprite = itemInfo.GetItemArt();
            amountText.text = amount.ToString();
        }

        public void OnPickedUp()
        {
            for (int i = 0; i < amount; i++)
                InventoryManager.Singleton.AddInventoryItem(itemInfo);

            Destroy(gameObject);
        }

        private void Update()
        {
            itemSprite.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
}