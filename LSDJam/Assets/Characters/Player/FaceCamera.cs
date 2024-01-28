using UnityEngine;

namespace Characters.Player
{
    public class FaceCamera : MonoBehaviour
    {
        private void LateUpdate() => transform.LookAt(GameObject.Find("Player").transform);
    }
}
