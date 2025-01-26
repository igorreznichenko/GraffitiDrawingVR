using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GraffitiDrawingVR.Runtime.VFX
{
	public class GraphicFader : Fader
	{
		[SerializeField]
		private Graphic _target;

		[SerializeField]
		private float _fadeTime;

		private Tween _fadeTween = null;

		private const float FADE_OUT_ALPHA_VALUE = 0;

		private const float FADE_IN_ALPHA_VALUE = 1;

		public override void FadeIn(Action callback)
		{
			DoFade(FADE_OUT_ALPHA_VALUE, FADE_IN_ALPHA_VALUE, _fadeTime, callback);
		}

		public override void FadeOut(Action callback)
		{
			DoFade(FADE_IN_ALPHA_VALUE, FADE_OUT_ALPHA_VALUE, _fadeTime, callback);
		}

		private void DoFade(float startAlpha, float endAlpha, float duration, Action callback)
		{
			_fadeTween?.Kill();

			Color color = _target.color;

			color.a = startAlpha;

			_target.color = color;

			_fadeTween = _target.DOFade(endAlpha, duration);
			_fadeTween.OnComplete(() => callback?.Invoke());
		}
	}
}
