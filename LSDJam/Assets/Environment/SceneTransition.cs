using System.Collections;
using Audio;
using Collectables;
using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class SceneTransition : Chest
    {
        private bool _isLoading;
        
        public override void OnInteract()
        {
            if (requiredItem != null)
            {
                for(var i = 0; i < Inventory.inventory.Count; i++)
                    if (Inventory.inventory[i].itemData.id == requiredItem.id)
                    {
                        _isLoading = true;
                        if (!AudioController.Singleton.effectsSource.isPlaying)
                            AudioController.Singleton.PlaySound(unlockedSound, 1f);
                        StartCoroutine(LoadNextScene());
                    }
            }

            if (lockedSound != null && !_isLoading)
            {
                if (!AudioController.Singleton.effectsSource.isPlaying)
                    AudioController.Singleton.PlaySound(lockedSound, 1f);
                requiredPrompt.enabled = true;                
            }
        }

        private IEnumerator LoadNextScene()
        {
            requiredPrompt.enabled = false;
            GameObject.Find("HUD").GetComponent<HUDController>().fade.Play("A_FadeOut");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
        }
    }
}
