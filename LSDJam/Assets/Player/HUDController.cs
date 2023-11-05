using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
    private FirstPersonController _player;
    public Image key;
    public Slider staminaMeter;

    private void Start()
    {
        _player = GetComponentInParent<FirstPersonController>();
        staminaMeter.value = _player.stamina;
        key.enabled = false;
    }

    void Update()
    {
        staminaMeter.value = _player.stamina;
        
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
