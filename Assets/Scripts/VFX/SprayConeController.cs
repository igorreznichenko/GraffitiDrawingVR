using UnityEngine;

namespace GraffitiDrawingVR.VFX
{
	public class SprayConeController : MonoBehaviour
	{
		[SerializeField]
		private float maxRange = 0.6f;

		private const float MIN_RANGE_VALUE = 0;

		private Material _material;

		private readonly int ColorId = Shader.PropertyToID("Color_686BCB55");
		private readonly int RangeId = Shader.PropertyToID("RangeV");

		private void Start()
		{
			_material = GetComponent<MeshRenderer>().material;
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