using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Extensions
{
	public static class RenderTextureExtension
	{
		public static void Fill(this RenderTexture renderTexture, Color color)
		{
			RenderTexture previous = RenderTexture.active;

			RenderTexture.active = renderTexture;

			GL.Clear(true, true, color);

			RenderTexture.active = previous;
		}
	}
}
