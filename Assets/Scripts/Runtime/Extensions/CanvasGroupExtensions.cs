using UnityEngine;

namespace GraffitiDrawingVR.Extensions
{
	public static class CanvasGroupExtensions
	{
		public static void ShowImmediately(this CanvasGroup canvasGroup)
		{
			canvasGroup.alpha = 1;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;
		}

		public static void HideImmediately(this CanvasGroup canvasGroup)
		{
			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
		}
	}
}
