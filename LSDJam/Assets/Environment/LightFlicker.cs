using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class LightFlicker : MonoBehaviour
    {
        private bool _isFlickering;
        private float _timeDelay;

        private void Update()
        {
            if (!_isFlickering)
                StartCoroutine(FlickeringLight());
        }

        private IEnumerator FlickeringLight()
        {
            _isFlickering = true;
            gameObject.GetComponent<Light>().enabled = false;
            _timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(_timeDelay);
            gameObject.GetComponent<Light>().enabled = true;
            _timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(_timeDelay);
            _isFlickering = false;
        }
    }
}
