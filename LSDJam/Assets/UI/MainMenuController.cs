using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject confirmationPrompt;

        private void Start() => HidePrompt();

        public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        public void LaunchPaulWebsite() => Application.OpenURL("https://paulharden.net/");
        public void LaunchMikeWebsite() => Application.OpenURL("https://www.mfgstudio.art/");

        public void ShowPrompt() => confirmationPrompt.SetActive(true);
        public void HidePrompt() => confirmationPrompt.SetActive(false);
        public void QuitGame() => Application.Quit();
    }
}
