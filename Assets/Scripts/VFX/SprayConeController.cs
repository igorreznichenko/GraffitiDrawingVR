using UnityEngine;

namespace GraffitiDrawingVR.VFX
{
	public class SprayConeController : MonoBehaviour
	{
		[SerializeField]
		private float maxRange = 0.6f;

		[Range(0, 1)]
		[SerializeField]
		private float _initialNormalizedRangeValue = 0;

		private const float MIN_RANGE_VALUE = 0;

		private Material _material;

		private readonly int ColorId = Shader.PropertyToID("Color_686BCB55");
		private readonly int RangeId = Shader.PropertyToID("RangeV");

		private void Awake()
		{
			_material = GetComponent<MeshRenderer>().material;
			SetRangeNormalized(_initialNormalizedRangeValue);
		}

		public void SetColor(Color color)
		{
			_material.SetColor(ColorId, color);
		}

		public void SetRangeNormalized(float range)
		{
			float value = Mathf.Lerp(MIN_RANGE_VALUE, maxRange, range);

			_material.SetFloat(RangeId, value);
		}
	}
}