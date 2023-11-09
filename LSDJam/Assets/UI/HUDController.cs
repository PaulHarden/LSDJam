using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private FirstPersonController _player;
    public Slider staminaMeter;
    public Slider pissMeter;
    public Image[] ItemSlots;

    private void Start()
    {
        _player = GetComponentInParent<FirstPersonController>();
        staminaMeter.value = _player.stamina;
        pissMeter.value = _player.piss;
        foreach (var itemSlot in ItemSlots)
            itemSlot.enabled = false;
    }

    void Update()
    {
        staminaMeter.value = _player.stamina;
        pissMeter.value = _player.piss;
    }
}
