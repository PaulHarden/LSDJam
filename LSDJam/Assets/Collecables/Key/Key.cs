using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    //public ItemData RewardItem;
    public float RotationSpeed;
    public AnimationCurve BobCurve;
    
    public static event HandleToothCollected OnKeyCollected;
    public delegate void HandleToothCollected(ItemData itemData);
    public ItemData keyData;

    void Update()
    {
        transform.Rotate(0, RotationSpeed, 0);
        transform.position = new Vector3(transform.position.x, BobCurve.Evaluate(Time.time % BobCurve.length), transform.position.z);
    }
    
    public void Collect()
    {
        OnKeyCollected?.Invoke(keyData);
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }*/
}
