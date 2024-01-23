using Player;
using UnityEngine;

namespace Environment.Door
{
    public class Door : Interactable
    {
        private Animator _anim;
        private AudioSource _audio;
        private bool _isOpen;
        public AudioClip openSound;
        public AudioClip closeSound;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _anim = GetComponent<Animator>();   
        }

        public override void OnInteract()
        {
            _isOpen = !_isOpen;
            _anim.SetTrigger("OpenClose");
        }

        private void OpenSound() => _audio.PlayOneShot(openSound);//AudioController.Singleton.PlaySound(openSound, 1f);
        private void CloseSound() => _audio.PlayOneShot(closeSound);//AudioController.Singleton.PlaySound(closeSound, 1f));
    }
}
