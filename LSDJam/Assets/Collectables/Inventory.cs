using System;
using System.Collections.Generic;
using UnityEngine;

namespace Collectables
{
    public class Inventory : MonoBehaviour
    {
        public static event Action<List<InventoryItem>> OnInventoryChange;
        public static List<InventoryItem> inventory = new();
        private Dictionary<ItemData, InventoryItem> _itemDictionary = new();

        private void OnEnable()
        {
            Chest.OnInteracted += Subtract;
            Tooth.Tooth.OnToothCollected += Add;
            Key.Key.OnKeyCollected += Add;
            Fish.Fish.OnFishCollected += Add;
            Pizza.PizzaSlice.OnPizzaCollected += Add;
        }

        private void OnDisable()
        {
            Chest.OnInteracted -= Subtract;
            Tooth.Tooth.OnToothCollected -= Subtract;
            Key.Key.OnKeyCollected -= Subtract;
            Fish.Fish.OnFishCollected -= Subtract;
            Pizza.PizzaSlice.OnPizzaCollected -= Subtract;
        }

        public void Add(ItemData itemData)
        {
            inventory.Clear();
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
                inventory.Clear();
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