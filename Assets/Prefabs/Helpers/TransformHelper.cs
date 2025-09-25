using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Helpers
{
	public class TransformHelper : MonoBehaviour
	{
		public void SetYLocalRotation(float rotation)
		{
			Vector3 rotationEuler = transform.localRotation.eulerAngles;
			rotationEuler.y = rotation;

			transform.localRotation = Quaternion.Euler(rotationEuler);
		}
	}
}
