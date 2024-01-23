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
        private Vector3 _offset = new(0,1.5f,0);

        private void Start() => requiredPrompt.enabled = false;
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
                        AudioController.Singleton.PlaySound(unlockedSound, 1f);
                        Instantiate(rewardItem, transform.position + _offset, Quaternion.identity);
                        Destroy(gameObject);
                        OnEndHover();
                        return;
                    }
                }
            }
            else
            {
                if (rewardItem != null)
                {
                    AudioController.Singleton.PlaySound(unlockedSound, 1f);
                    Instantiate(rewardItem, transform.position + _offset, Quaternion.identity);
                    Destroy(gameObject);
                    OnEndHover();
                    return;
                }
            }

            if (!AudioController.Singleton.effectsSource.isPlaying)
                AudioController.Singleton.PlaySound(lockedSound, 1f);
            requiredPrompt.enabled = true;
        }
    }
}
