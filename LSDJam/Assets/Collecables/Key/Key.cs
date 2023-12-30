using UnityEngine;

namespace Collecables.Key
{
    public class Key : MonoBehaviour, ICollectable
    {
        //public ItemData RewardItem;
        public float rotationSpeed;
        public AnimationCurve bobCurve;
    
        public static event HandleToothCollected OnKeyCollected;
        public delegate void HandleToothCollected(ItemData itemData);
        public ItemData keyData;

        void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }
    
        public void Collect()
        {
            OnKeyCollected?.Invoke(keyData);
            Destroy(gameObject);
        }

        /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }*/
    }
}
