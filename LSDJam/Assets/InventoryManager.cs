using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new (4);

    private void OnEnable() => Inventory2.OnInventoryChange += DrawInventory;
    private void OnDisable() => Inventory2.OnInventoryChange -= DrawInventory;

    private void ResetInventory()
    {
        foreach (Transform slots in transform)
            Destroy(slots.gameObject);
        inventorySlots = new List<InventorySlot>(4);
    }

    private void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for (int i = 0; i < inventorySlots.Capacity; i++)
            CreateInventorySlot();

        for (int i = 0; i < inventory.Count; i++) // Error if capacity is exceeded!
            inventorySlots[i].DrawSlots(inventory[i]);
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, transform, false);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        inventorySlots.Add(newSlotComponent);
    }
}
