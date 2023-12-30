using Collecables;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnInteract : MonoBehaviour, IInteract
{
    public Image image;
    public InventoryItem item;
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
        item.AddQuantity();
        Destroy(gameObject);
    }

    public void OnEndHover() => Debug.Log($"{gameObject.name} has been destroyed!");
}
