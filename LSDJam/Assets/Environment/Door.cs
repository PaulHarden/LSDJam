using Player;
using UnityEngine;

namespace Environment
{
    public class Door : Interactable
    {
        private Animator _anim;
        private bool _isOpen;

        private void Start() => _anim = GetComponent<Animator>();
        
        public override void OnInteract()
        {
            if (_anim.GetBool("Open"))
                _anim.SetBool("Open", false);
            else
                _anim.SetBool("Open", true);
        }
    }
}
