using System.Collections;
using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class SceneTransition : Interactable
    {
        public override void OnInteract() => StartCoroutine(LoadNextScene());

        private IEnumerator LoadNextScene()
        {
            GameObject.Find("HUD").GetComponent<HUDController>().fade.Play("A_FadeOut");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
        }
    }
}
