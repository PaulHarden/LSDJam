using UnityEngine;

namespace Collecables.Tooth
{
    public class Tooth : MonoBehaviour, ICollectable
    {
        public float rotationSpeed;
        public AnimationCurve bobCurve;
        public static event HandleToothCollected OnToothCollected;
        public delegate void HandleToothCollected(ItemData itemData);
        public ItemData toothData;
    
        void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }
    
        public void Collect()
        {
            OnToothCollected?.Invoke(toothData);
            Destroy(gameObject);
        }
    }
}
