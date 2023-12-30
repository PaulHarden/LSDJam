using Collecables;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Interactable : MonoBehaviour, IInteract
    {
        public static event HandleInteract OnInteracted;
        public delegate void HandleInteract(ItemData item);
        public Image interactPrompt;
        public Image requiredPrompt;
        public ItemData requiredItem;
        public GameObject rewardItem;
        public AudioClip unlockedSfx;
        public AudioClip lockedSfx;
        private Vector3 _offset = new(0,1f,0);
        private const float _maxRange = 100f;
        public float MaxRange
        {
            get { return _maxRange; }
        }

        private void Start()
        {
            interactPrompt.enabled = false;
            requiredPrompt.enabled = false;
        }
    
        public void OnStartHover() => interactPrompt.enabled = true;

        public virtual void OnInteract()
        {
            Debug.Log("Player interacted with " + gameObject.name);
            if (requiredItem != null)
            {
                for(var i = 0; i < Inventory.inventory.Count; i++)
                {
                    if (Inventory.inventory[i].itemData.id == requiredItem.id)
                    {
                        OnInteracted?.Invoke(Inventory.inventory[i].itemData);
                        //AudioController.Singleton.PlaySound(UnlockedSFX, 0.25f);
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
                    Instantiate(rewardItem, transform.position + _offset, Quaternion.identity);
                    Destroy(gameObject);
                    OnEndHover();
                    return;
                }
            }
            //AudioController.Singleton.PlaySound(LockedSFX, 0.25f);
            requiredPrompt.enabled = true;
        }

        public void OnEndHover()
        {
            interactPrompt.enabled = false;
            requiredPrompt.enabled = false;
        }
    }
}
