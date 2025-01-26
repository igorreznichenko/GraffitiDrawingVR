using UnityEngine;

namespace GraffitiDrawingVR.Drawing
{
	public interface IDrawer
	{
		public void SetColor(Color color);

		public void Draw(float strengthNormalized);
	}
}