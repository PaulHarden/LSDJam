using Player;
using UnityEngine;

namespace Environment.Door
{
    public class Door : Interactable
    {
        private Animator _anim;
        private bool _isOpen;

        private void Start() => _anim = GetComponent<Animator>();
        
        public override void OnInteract() => _anim.SetTrigger("OpenClose");
    }
}
