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
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                _anim.SetTrigger("OpenClose");    
        }

        private void OpenSound() => _audio.PlayOneShot(openSound);
        private void CloseSound() => _audio.PlayOneShot(closeSound);
    }
}
