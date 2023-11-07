using UnityEngine;

public class Tooth : MonoBehaviour, ICollectable
{
    public static event HandleToothCollected OnToothCollected;
    public delegate void HandleToothCollected(ItemData itemData);
    public ItemData toothData;
    
    public void Collect()
    {
        OnToothCollected?.Invoke(toothData);
        Destroy(gameObject);
    }
}
