using System.Collections;
using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class EndGame : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
                StartCoroutine(LoadNextScene());
        }

        private IEnumerator LoadNextScene()
        {
            GameObject.Find("HUD").GetComponent<HUDController>().fade.Play("A_FadeOut");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
        }
    }
}
