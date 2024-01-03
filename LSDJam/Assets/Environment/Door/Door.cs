using Audio;
using Player;
using UnityEngine;

namespace Environment.Door
{
    public class Door : Interactable
    {
        private Animator _anim;
        private bool _isOpen;
        public AudioClip openSound;
        public AudioClip closeSound;

        private void Start() => _anim = GetComponent<Animator>();

        public override void OnInteract()
        {
            _isOpen = !_isOpen;
            _anim.SetTrigger("OpenClose");
        }

        private void OpenSound() => AudioController.Singleton.PlaySound(openSound, 1f);
        private void CloseSound() => AudioController.Singleton.PlaySound(closeSound, 1f);
    }
}
