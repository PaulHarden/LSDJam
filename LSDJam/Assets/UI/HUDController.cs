using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUDController : MonoBehaviour
    {
        private FirstPersonController _player;
        public Slider staminaMeter;
        public Slider pissMeter;
        public Image[] itemSlots;
        public GameObject pauseMenu;

        private void Start()
        {
            _player = GetComponentInParent<FirstPersonController>();
            staminaMeter.value = _player.stamina;
            pissMeter.value = _player.piss;
            foreach (var itemSlot in itemSlots)
                itemSlot.enabled = false;
        }

        private void Update()
        {
            staminaMeter.value = _player.stamina;
            pissMeter.value = _player.piss;

            if (_player.isPaused)
                pauseMenu.SetActive(true);
            else
                pauseMenu.SetActive(false);
        }
    }
}
