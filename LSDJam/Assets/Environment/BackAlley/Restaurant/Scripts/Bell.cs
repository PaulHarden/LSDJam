using Characters.Player;
using UnityEngine;

namespace Environment.BackAlley.Restaurant.Scripts
{
    public class Bell : Interactable
    {
        private AudioSource _audio;
        public AudioClip bellSound;

        private void Start() => _audio = GetComponent<AudioSource>();

        public override void OnInteract()
        {
            if (!_audio.isPlaying)
                _audio.PlayOneShot(bellSound);
        }
    }
}
