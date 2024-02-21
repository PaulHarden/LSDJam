using UnityEngine;

namespace UI
{
    public class FullScreen : MonoBehaviour
    {
        public void ToggleFullscreen()
        {
            Screen.SetResolution(1920, 1080, false);
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}