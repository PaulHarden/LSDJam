using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player
{
    public class Interactable : MonoBehaviour, IInteract
    {
        public Image interactPrompt;
        private const float _maxRange = 100f;
        public float MaxRange
        {
            get { return _maxRange; }
        }
        private void Start() => interactPrompt.enabled = false;
        public void OnStartHover() => interactPrompt.enabled = true;
        public virtual void OnEndHover() => interactPrompt.enabled = false;
        public virtual void OnInteract() {}
    }
}
