using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private FirstPersonController _player;
    public Slider staminaMeter;
    public Slider pissMeter;
    public Image[] ItemSlots;
    public Sprite key;

    //public TextMeshProUGUI toothQuantityText;
    //private int toothQuantity;
    
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
        
        for (var i = 0; i < Inventory.inventory.Count; i++)
            EnableImage(Inventory.inventory[i].id);
    }

    private void EnableImage(int num)
    {
        ItemSlots[0].enabled = true;
        switch (num)
        {
            case 1:
                ItemSlots[0].sprite = key;
                Debug.Log("inventory slot #1 is full!");
                break;
        }
    }

    /*public void IncrementToothQuantity()
    {
        toothQuantity++;
        toothQuantityText.text = $"Teeth: {toothQuantity}";
    }*/
}
