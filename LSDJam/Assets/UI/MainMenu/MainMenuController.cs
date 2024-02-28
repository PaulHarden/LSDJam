using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject confirmationPrompt;

        private void Start()
        {
            HideOptions();
            HidePrompt();
        }
        
        public void ShowOptions()
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        public void HideOptions()
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        public void ShowPrompt()
        {
            confirmationPrompt.SetActive(true);
            mainMenu.SetActive(false);
        }
        public void HidePrompt()
        {
            confirmationPrompt.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        public void LaunchPaulWebsite() => Application.OpenURL("https://paulharden.net/");
        public void LaunchMikeWebsite() => Application.OpenURL("https://www.mfgstudio.art/");
        public void QuitGame() => Application.Quit();
    }
}
