using UnityEngine;

namespace Environment
{
    public class Train : MonoBehaviour
    {
        public GameObject train;
        public float speed;
        private bool _isActive;

        private void Start() => train.SetActive(false);

        private void Update()
        {
            if (_isActive)
                train.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                train.SetActive(true);
                _isActive = true;
            }
        }
    }
}
