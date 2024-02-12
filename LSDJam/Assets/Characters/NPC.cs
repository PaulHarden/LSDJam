using UnityEngine;

namespace Characters
{
    public class NPC : MonoBehaviour
    {
        private Animator _anim;
        public GameObject goal;
        private void Start() => _anim = GetComponent<Animator>();

        private void Update()
        {
            if (goal != null)
                if (goal.activeInHierarchy)
                    _anim.SetTrigger($"Happy");
        }

        private void OnParticleCollision(GameObject other) => _anim.SetTrigger($"Angry");
    }
}
