using UnityEngine;

namespace Environment.Skyboxes
{
	public class SkyboxRotate : MonoBehaviour
	{
		public float rotateSpeed;
		public void Update() =>
			RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
	}
}
