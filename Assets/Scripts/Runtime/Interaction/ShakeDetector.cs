using UnityEngine;
using UnityEngine.Events;

namespace GraffitiDrawingVR.Runtime.Interaction
{
	public class ShakeDetector : MonoBehaviour
	{
		[SerializeField]
		private Transform _trackable;

		[SerializeField]
		private float _shakeSpeed;

		private bool _reachedShakeSpeed;

		[SerializeField]
		private UnityEvent _shakeEvent;

		Vector3 _previousPosition;

		public event UnityAction ShakeEvent
		{
			add => _shakeEvent.AddListener(value);
			remove => _shakeEvent.RemoveListener(value);
		}

		private void Start()
		{
			_previousPosition = _trackable.position;
		}

		private void Update()
		{
			Vector3 currentPosition = _trackable.position;

			Vector3 direction = currentPosition - _previousPosition;

			float distance = direction.magnitude;

			float velocity = distance / Time.deltaTime;

			if (velocity >= _shakeSpeed)
			{
				_reachedShakeSpeed = true;
			}
			else if (_reachedShakeSpeed)
			{
				_reachedShakeSpeed = false;

				_shakeEvent?.Invoke();
			}

			_previousPosition = currentPosition;
		}
	}
}