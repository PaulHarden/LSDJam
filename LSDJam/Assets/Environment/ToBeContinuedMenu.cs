using Characters.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class ToBeContinuedMenu : MonoBehaviour
    {
        public StarterAssetsInputs input;
        private void Start()
        {
            input.SetCursorState(false);
            input.cursorLocked = false;
            input.cursorInputForLook = false;
        }
        public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
        public void QuitGame() => Application.Quit();
    }
}
