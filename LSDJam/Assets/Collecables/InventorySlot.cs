using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantity;

    public void ClearSlot()
    {
        icon.enabled = false;
        quantity.enabled = false;
    }

    public void DrawSlots(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        quantity.enabled = true;

        icon.sprite = item.itemData.icon;
        quantity.text = item.itemQuantity.ToString();
    }
}
