using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour
{
    public Image blue;
    public Image green;
    public Image red;
    public Image gold;

    private void Start()
    {
    }
    
    void Update()
    {
        for (var i = 0; i < InventoryScript.inventory.Count; i++)
        {
            EnableImage(InventoryScript.inventory[i].id);
        }
    }

    private void EnableImage(int num)
    {
        switch (num)
        {
            case 1:
                blue.enabled = true;
                break;
            case 2:
                green.enabled = true;
                break;
            case 3:
                red.enabled = true;
                break;
            case 4:
                gold.enabled = true;
                break;
        }
    }
}
