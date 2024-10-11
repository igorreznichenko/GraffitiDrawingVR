using UnityEngine;

namespace GraffitiDrawingVR.Drawing
{
	public class DrawerController : MonoBehaviour
	{
		[SerializeField]
		private Drawable[] _drawables;

		[SerializeField]
		private Drawer[] _drawers;

		[SerializeField]
		private float _minIntensity;

		[SerializeField]
		private float _maxIntensity;

		public void Draw(float drawStrength)
		{
			foreach (var drawable in _drawables)
			{
				foreach (var drawer in _drawers)
				{
					drawer.Intencity = Mathf.Lerp(_minIntensity, _maxIntensity, drawStrength);

					drawer.Draw(drawable);
				}
			}
		}
	}
}