using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public int quantity;
    
    public void AddItem(Item newItem)
    {
        Inventory.inventory.Add(newItem);
        foreach (var item in Inventory.inventory.Where(item => item.id == newItem.id))
        {
            item.quantity++;
            Debug.Log(newItem.quantity + " " + newItem + " in inventory");
            return;
        }
    }

    public void LoseItem(Item item)
    {
        for(var i = 0; i < Inventory.inventory.Count; i++)
        {
            if (Inventory.inventory[i] == item)
                Inventory.inventory.RemoveAt(i);
        }
    }

    public void DisplayItem(Image image) => image.enabled = true;
}
