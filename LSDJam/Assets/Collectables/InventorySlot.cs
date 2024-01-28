using UnityEngine;
using UnityEngine.UI;

namespace Collectables
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;

        public void ClearSlot() => icon.enabled = false;

        public void DrawSlots(InventoryItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }
            
            icon.enabled = true;
            icon.sprite = item.itemData.icon;
        }
    }
}
