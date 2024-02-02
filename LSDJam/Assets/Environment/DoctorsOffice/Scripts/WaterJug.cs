using UnityEngine;

namespace Environment.DoctorsOffice.Scripts
{
    public class WaterJug : MonoBehaviour
    {
        private ParticleSystem _ps;
        private void Start()
        {
            _ps = GetComponentInChildren<ParticleSystem>();
            _ps.gameObject.SetActive(false);
        }
        private void OnCollisionExit(Collision other) => _ps.gameObject.SetActive(true);
    }
}
