using UnityEngine;

public class Tooth : MonoBehaviour, ICollectable
{
    public float RotationSpeed;
    public AnimationCurve BobCurve;
    public static event HandleToothCollected OnToothCollected;
    public delegate void HandleToothCollected(ItemData itemData);
    public ItemData toothData;
    
    void Update()
    {
        transform.Rotate(0, RotationSpeed, 0);
        transform.position = new Vector3(transform.position.x, BobCurve.Evaluate(Time.time % BobCurve.length), transform.position.z);
    }
    
    public void Collect()
    {
        OnToothCollected?.Invoke(toothData);
        Destroy(gameObject);
    }
}
