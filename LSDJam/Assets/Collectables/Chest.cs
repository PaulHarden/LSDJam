using Audio;
using Characters.Player;
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
        private bool _itemDispensed;
        public AudioClip unlockedSound;
        public AudioClip lockedSound;
        public Vector3 offset;
        public bool destroyOnInteract;
        public bool changeOnInteract;
        public GameObject changedObject;
        
        private void Start()
        {
            if (requiredPrompt != null)
                requiredPrompt.enabled = false;

            if (changeOnInteract && changedObject != null)
                changedObject.SetActive(false);
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

                            if (changeOnInteract)
                                if (changedObject != null)
                                    changedObject.SetActive(true);
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

            if (GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger($"Open");
            
            if (lockedSound != null)
                if (!AudioController.Singleton.effectsSource.isPlaying)
                    AudioController.Singleton.PlaySound(lockedSound, 1f);
            if (requiredItem != null && _itemDispensed == false)
                requiredPrompt.enabled = true;
        }
    }
}
