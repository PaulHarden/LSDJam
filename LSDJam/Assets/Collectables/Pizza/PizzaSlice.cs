using Audio;
using UnityEngine;

namespace Collectables.Pizza
{
    public class PizzaSlice : MonoBehaviour, ICollectable
    {
        public ItemData pizzaData;
        public float rotationSpeed;
        public AnimationCurve bobCurve;
        public AudioClip collectSound;
        public static event HandlePizzaCollected OnPizzaCollected;
        public delegate void HandlePizzaCollected(ItemData itemData);

        public void Update()
        {
            transform.Rotate(0, rotationSpeed, 0);
            transform.position = new Vector3(transform.position.x, bobCurve.Evaluate(Time.time % bobCurve.length), transform.position.z);
        }

        public void Collect()
        {
            OnPizzaCollected?.Invoke(pizzaData);
            AudioController.Singleton.PlaySound(collectSound, 1f);
            Destroy(transform.parent.gameObject);
        }
    }
}
