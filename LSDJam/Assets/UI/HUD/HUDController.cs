using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        private FirstPersonController _player;
        //public Slider healthMeter;
        public Slider staminaMeter;
        public Slider pissMeter;
        public Image[] itemSlots;
        public GameObject pauseMenu;
        public Animator fade;

        private void Start()
        {
            _player = GetComponentInParent<FirstPersonController>();
            foreach (var itemSlot in itemSlots)
                itemSlot.enabled = false;
            fade.Play("A_FadeIn");
        }

        private void Update()
        {
            //healthMeter.value = _player.health;
            staminaMeter.value = _player.stamina;
            pissMeter.value = _player.piss;

            if (_player.isPaused)
                pauseMenu.SetActive(true);
            else
                pauseMenu.SetActive(false);
        }
    }
}
