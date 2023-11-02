using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
    public Image key;

    private void Start() => key.enabled = false;

    void Update()
    {
        for (var i = 0; i < Inventory.inventory.Count; i++)
            EnableImage(Inventory.inventory[i].id);
    }

    private void EnableImage(int num)
    {
        switch (num)
        {
            case 1:
                key.enabled = true;
                break;
        }
    }
}
