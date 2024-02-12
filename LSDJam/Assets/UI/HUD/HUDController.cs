using Characters.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        private FirstPersonController _player;
        //public Slider healthMeter;
        public Slider staminaMeter;
        public Slider pissMeter;
        public Image flashlightIcon;
        public Sprite flashlightOn, flashlightOff;
        public Image[] itemSlots;
        public GameObject pauseMenu;
        public Animator fade;
        public GameObject confirmationPrompt;

        private void Start()
        {
            HidePrompt();
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

            if (_player.flashlight.activeInHierarchy)
                flashlightIcon.sprite = flashlightOn;
            else
                flashlightIcon.sprite = flashlightOff;
        }

        public void ShowPrompt() => confirmationPrompt.SetActive(true);
        public void HidePrompt() => confirmationPrompt.SetActive(false);

        public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
    }
}
