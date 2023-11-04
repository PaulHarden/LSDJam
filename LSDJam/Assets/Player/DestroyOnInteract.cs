using UnityEngine;
using UnityEngine.UI;

public class DestroyOnInteract : MonoBehaviour, Interactable
{
    public Image image;
    public Item item;
    private const float maxRange = 100f;
    public float MaxRange
    {
        get { return maxRange; }
    }

    private void Start() => image.enabled = false;
    
    public void OnStartHover()
    {
        Debug.Log($"Ready to destroy {gameObject.name}");
    }

    public void OnInteract()
    {
        item.AddItem(item);
        item.DisplayItem(image);
        Destroy(gameObject);
    }

    public void OnEndHover() => Debug.Log($"{gameObject.name} has been destroyed!");
}
