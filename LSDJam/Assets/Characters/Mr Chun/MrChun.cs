using UnityEngine;

namespace Characters.Mr_Chun
{
    public class MrChun : MonoBehaviour
    {
        private Animator _anim;
        public GameObject goal;
        private void Start() => _anim = GetComponent<Animator>();

        private void Update()
        {
            if (goal.activeInHierarchy)
                _anim.SetTrigger($"Happy");
        }

        private void OnParticleCollision(GameObject other) => _anim.SetTrigger($"Angry");
    }
}
