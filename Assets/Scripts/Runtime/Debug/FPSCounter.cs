using TMPro;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.DebugScripts
{
	public class FPSCounter : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _text;

		[SerializeField]
		private float _refreshTime;

		private float _timer;

		private const string FPS_OUTPUT_FORMAT = "{0} FPS";

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		private void Update()
		{
			if (Time.unscaledTime > _timer)
			{
				int fps = (int)(1f / Time.unscaledDeltaTime);

				_text.text = string.Format(FPS_OUTPUT_FORMAT, fps);

				_timer = Time.unscaledTime + _refreshTime;
			}
		}
	}
}
