using Audio;
using UnityEngine;

namespace Environment
{
    public class KillPlane : MonoBehaviour
    {
        public Transform respawnPoint;
        public AudioClip killSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!AudioController.Singleton.effectsSource.isPlaying)
                    AudioController.Singleton.PlaySound(killSound, 1f);
                var cc = other.GetComponent<CharacterController>();
                cc.enabled = false;
                other.transform.position = respawnPoint.transform.position;
                cc.enabled = true;
            }
        }
    }
}
