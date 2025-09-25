using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Drawing
{
	public interface IDrawer
	{
		public void SetColor(Color color);

		public void Draw(float strengthNormalized);
	}
}