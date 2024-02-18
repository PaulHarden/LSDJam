using Audio;
using UnityEngine;

namespace Collectables.Tooth
{
    public class Tooth : MonoBehaviour, ICollectable
    {
        public ItemData toothData;
        public float rotationSpeed;
        public AnimationCurve bobCurve;
        public AudioClip collectSound;
        public static event HandleToothCollected OnToothCollected;
        public delegate void HandleToothCollected(ItemData itemData);
    
        private void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }
    
        public void Collect()
        {
            OnToothCollected?.Invoke(toothData);
            AudioController.Singleton.PlaySound(collectSound, 1f);
            Destroy(transform.parent.gameObject);
        }
    }
}
