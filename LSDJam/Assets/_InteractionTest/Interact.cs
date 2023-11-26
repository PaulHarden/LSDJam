using Audio;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour, Interactable
{
    public Image InteractPrompt;
    public Image RequiredPrompt;
    public Item RequiredItem;
    public GameObject RewardItem;
    public AudioClip UnlockedSFX;
    public AudioClip LockedSFX;
    private Vector3 _offset = new(0,1.5f,0);
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
                if (Inventory.inventory[i].id == RequiredItem.id)
                {
                    //AudioController.Singleton.PlaySound(UnlockedSFX, 0.25f);
                    Instantiate(RewardItem, transform.position + _offset, Quaternion.Euler(0, 90, -90));
                    Destroy(gameObject);
                    return;
                }
            }    
        }
        else
        {
            Instantiate(RewardItem, transform.position + _offset, Quaternion.Euler(0, 90, -90));
            Destroy(gameObject);
            return;
        }
        //AudioController.Singleton.PlaySound(LockedSFX, 0.25f);
        RequiredPrompt.enabled = true;
    }

    public void OnEndHover()
    {
        InteractPrompt.enabled = false;
        RequiredPrompt.enabled = false;
    }
}
