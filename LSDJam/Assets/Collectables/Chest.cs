using Audio;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Collectables
{
    public class Chest : Interactable
    {
        public static event HandleInteract OnInteracted;
        public delegate void HandleInteract(ItemData item);
        public Image requiredPrompt;
        public ItemData requiredItem;
        public GameObject rewardItem;
        public AudioClip unlockedSound;
        public AudioClip lockedSound;
        public Vector3 offset;
        public bool destroyOnInteract;
        private bool _itemDispensed;

        private void Start()
        {
            if (requiredPrompt != null)
                requiredPrompt.enabled = false;
        }

        public override void OnEndHover()
        {
            interactPrompt.enabled = false;
            requiredPrompt.enabled = false;
        }
    
        public override void OnInteract()
        {
            Debug.Log("Player interacted with " + gameObject.name);
            if (requiredItem != null)
            {
                for(var i = 0; i < Inventory.inventory.Count; i++)
                {
                    if (Inventory.inventory[i].itemData.id == requiredItem.id)
                    {
                        OnInteracted?.Invoke(Inventory.inventory[i].itemData);
                        if (!AudioController.Singleton.effectsSource.isPlaying)
                            AudioController.Singleton.PlaySound(unlockedSound, 1f);
                        if (!_itemDispensed)
                        {
                            Instantiate(rewardItem, transform.position + offset, Quaternion.identity);
                            _itemDispensed = true;
                            if (destroyOnInteract)
                                Destroy(gameObject);    
                        }
                        OnEndHover();
                        return;
                    }
                }
            }
            else
            {
                if (rewardItem != null)
                {
                    if (!AudioController.Singleton.effectsSource.isPlaying)
                        AudioController.Singleton.PlaySound(unlockedSound, 1f);
                    if (!_itemDispensed)
                    {
                        Instantiate(rewardItem, transform.position + offset, Quaternion.identity);
                        _itemDispensed = true;
                        if (destroyOnInteract)
                            Destroy(gameObject);                        
                    }
                    OnEndHover();
                    return;
                }
            }

            if (lockedSound != null)
                if (!AudioController.Singleton.effectsSource.isPlaying)
                    AudioController.Singleton.PlaySound(lockedSound, 1f);
            requiredPrompt.enabled = true;
        }
    }
}
