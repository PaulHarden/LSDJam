using Audio;
using UnityEngine;

namespace Collectables.Fish
{
    public class Fish : MonoBehaviour, ICollectable
    {
        public ItemData fishData;
        public float rotationSpeed;
        public AnimationCurve bobCurve;
        public AudioClip collectSound;
        public static event HandleFishCollected OnFishCollected;
        public delegate void HandleFishCollected(ItemData itemData);

        private void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }

        public void Collect()
        {
            OnFishCollected?.Invoke(fishData);
            AudioController.Singleton.PlaySound(collectSound, 1f);
            Destroy(gameObject);
        }
    }
}
