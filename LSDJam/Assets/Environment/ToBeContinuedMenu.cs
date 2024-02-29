using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class ToBeContinuedMenu : MonoBehaviour
    {
        public void ReturnToMainMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
