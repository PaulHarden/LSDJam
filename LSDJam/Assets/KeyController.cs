using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Item RewardItem;
    public float RotationSpeed;
    public AnimationCurve BobCurve;

    void Update()
    {
        transform.Rotate(RotationSpeed, 0, 0);
        transform.position = new Vector3(transform.position.x, BobCurve.Evaluate(Time.time % BobCurve.length), transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<AudioManager>().PlaySound("pickup");
            RewardItem.AddItem(RewardItem);
            Destroy(gameObject);
        }
    }
}
