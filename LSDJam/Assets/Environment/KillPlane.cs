using UnityEngine;

namespace Environment
{
    public class KillPlane : MonoBehaviour
    {
        public Transform respawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cc = other.GetComponent<CharacterController>();
                cc.enabled = false;
                other.transform.position = respawnPoint.transform.position;
                cc.enabled = true;
            }
        }
    }
}
