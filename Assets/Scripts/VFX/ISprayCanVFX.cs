using UnityEngine;

namespace GraffitiDrawingVR.VFX
{
	public interface ISprayCanVFX
	{
		public void SetColor(Color color);

		public void StartVFX();

		public void StopVFX();

		public void UpdateSprayForce(float value);
	}
}