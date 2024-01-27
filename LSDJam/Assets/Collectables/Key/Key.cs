using Audio;
using UnityEngine;

namespace Collectables.Key
{
    public class Key : MonoBehaviour, ICollectable
    {
        public ItemData keyData;
        public float rotationSpeed;
        public AnimationCurve bobCurve;
        public AudioClip collectSound;
        public static event HandleToothCollected OnKeyCollected;
        public delegate void HandleToothCollected(ItemData itemData);

        private void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }
    
        public void Collect()
        {
            OnKeyCollected?.Invoke(keyData);
            AudioController.Singleton.PlaySound(collectSound, 1f);
            Destroy(transform.parent.gameObject);
        }
    }
}
