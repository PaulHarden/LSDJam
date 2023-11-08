﻿using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour, IInteract
{
    //public AudioManager audioManager;
    public Image InteractPrompt;
    public Image RequiredPrompt;
    public ItemData RequiredItem;
    public GameObject RewardItem;
    private Vector3 _offset = new(0,1f,0);
    private const float maxRange = 100f;
    public float MaxRange
    {
        get { return maxRange; }
    }

    private void Start()
    {
        InteractPrompt.enabled = false;
        RequiredPrompt.enabled = false;
    }
    
    public void OnStartHover() => InteractPrompt.enabled = true;

    public void OnInteract()
    {
        Debug.Log("Player interacted with " + gameObject.name);
        if (RequiredItem != null)
        {
            for(var i = 0; i < Inventory.inventory.Count; i++)
            {
                if (Inventory.inventory[i].itemData.id == RequiredItem.id)
                {
                    //audioManager.PlaySound("interact");
                    Instantiate(RewardItem, transform.position + _offset, Quaternion.identity); //Quaternion.Euler(0, 90, -90));
                    Destroy(gameObject);
                    return;
                }
            }    
        }
        else
        {
            Instantiate(RewardItem, transform.position + _offset, Quaternion.identity); //Quaternion.Euler(0, 90, -90));
            Destroy(gameObject);
            return;
        }

        //audioManager.PlaySound("noKey");
        RequiredPrompt.enabled = true;
    }

    public void OnEndHover()
    {
        InteractPrompt.enabled = false;
        RequiredPrompt.enabled = false;
    }
}
