using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace GraffitiDrawingVR.Runtime.VFX
{
	public class SprayCanVFX : MonoBehaviour, ISprayCanVFX
	{
		[SerializeField]
		private SprayConeController _sprayConeController;

		[SerializeField]
		private ParticleSystem _gas;

		public void SetColor(Color color)
		{
			_sprayConeController.SetColor(color);
			SetParticleColorIgnoreAlpha(color);
		}

		private void SetParticleColorIgnoreAlpha(Color color)
		{
			MainModule main = _gas.main;

			color.a = main.startColor.color.a;

			main.startColor = color;
		}

		public void UpdateSprayForce(float value)
		{
			_sprayConeController.SetRangeNormalized(value);
		}

		public void StartVFX()
		{
			_gas.Play();
		}

		public void StopVFX()
		{
			_gas.Stop();
		}
	}
}