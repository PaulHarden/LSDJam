using System.Collections;
using Collectables;
using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class SceneTransition : Interactable
    {
        public ItemData requiredItem;
        
        public override void OnInteract()
        {
            if (requiredItem != null)
                for(var i = 0; i < Inventory.inventory.Count; i++)
                    if (Inventory.inventory[i].itemData.id == requiredItem.id)
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
