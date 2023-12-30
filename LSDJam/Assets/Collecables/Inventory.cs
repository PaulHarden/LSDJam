using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Collecables
{
    public class Inventory : MonoBehaviour
    {
        public static event Action<List<InventoryItem>> OnInventoryChange;
        public static List<InventoryItem> inventory = new();
        private Dictionary<ItemData, InventoryItem> _itemDictionary = new();

        private void OnEnable()
        {
            Interactable.OnInteracted += Subtract;
            Tooth.Tooth.OnToothCollected += Add;
            Key.Key.OnKeyCollected += Add;
        }

        private void OnDisable()
        {
            Interactable.OnInteracted -= Subtract;
            Tooth.Tooth.OnToothCollected -= Add;
            Key.Key.OnKeyCollected -= Add;
        }

        public void Add(ItemData itemData)
        {
            // TODO: check if going over available slots capacity!
            if (_itemDictionary.TryGetValue(itemData, out InventoryItem item))
            {
                item.AddQuantity();
                OnInventoryChange?.Invoke(inventory);
                Debug.Log($"{item.itemData.displayName} {item.itemQuantity}");
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                _itemDictionary.Add(itemData, newItem);
                OnInventoryChange?.Invoke(inventory);
                Debug.Log($"{itemData.displayName} added to inventory!");
            }
        }

        public void Subtract(ItemData itemData)
        {
            if (_itemDictionary.TryGetValue(itemData, out InventoryItem item))
            {
                item.SubtractQuantity();
                if (item.itemQuantity == 0)
                {
                    inventory.Remove(item);
                    _itemDictionary.Remove(itemData);
                    Debug.Log($"{item.itemData.displayName} removed from inventory!");
                }
                OnInventoryChange?.Invoke(inventory);
            }
        }
    }
}