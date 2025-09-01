using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GraffitiDrawingVR.Runtime.UI
{
	public class ScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		private float _scaleCoefficient;

		[SerializeField]
		private float _scaleTime = 0.5f;

		private Vector3 _normalScale;

		private Tween _scalingTween;

		private void Start()
		{
			_normalScale = transform.localScale;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Vector3 targetScale = _normalScale * _scaleCoefficient;

			DoScale(targetScale);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			DoScale(_normalScale);
		}

		private void DoScale(Vector3 targetScale)
		{
			_scalingTween?.Kill();
			_scalingTween = transform.DOScale(targetScale, _scaleTime);
		}
	}
}
