using GraffitiDrawingVR.Constants;
using GraffitiDrawingVR.Input;
using UnityEngine;
using UnityEngine.Events;

namespace GraffitiDrawingVR.SprayCan
{
	public class SprayCan : MonoBehaviour
	{
		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private GameObject _triggerInputValueGameObject;

		[Range(0, 1)]
		[SerializeField]
		private float _triggerSprayPinch = 0.3f;

		private ITriggerInputValue _triggerInputValue;

		private int _triggerInputValueKey = Animator.StringToHash(AnimatorVariableName.APRAY_CAN_TRIGGER_INPUT_VALUE);

		private bool _isSpray = false;

		public bool IsSpray
		{
			get { return _isSpray; }
			protected set
			{
				if (_isSpray != value)
				{
					_isSpray = value;

					if (_isSpray)
					{
						_startSprayEvent?.Invoke();
					}
					else
					{
						_stopSprayEvent?.Invoke();
					}
				}
			}
		}

		#region Events
		[SerializeField]
		private UnityEvent _startSprayEvent;

		public event UnityAction StartSprayEvent
		{
			add { _startSprayEvent.AddListener(value); }
			remove { _startSprayEvent.RemoveListener(value); }
		}

		[SerializeField]
		private UnityEvent _stopSprayEvent;

		public event UnityAction StopSprayEvent
		{
			add { _stopSprayEvent.AddListener(value); }
			remove { _stopSprayEvent.RemoveListener(value); }
		}

		[SerializeField]
		private UnityEvent<float> _sprayForceUpdateEvent;

		public event UnityAction<float> SprayForceUpdateEvent
		{
			add { _sprayForceUpdateEvent.AddListener(value); }
			remove { _sprayForceUpdateEvent.RemoveListener(value); }
		}
		#endregion

		private void Awake()
		{
			_triggerInputValue = _triggerInputValueGameObject.GetComponent<ITriggerInputValue>();
		}

		private void Update()
		{
			float input = _triggerInputValue.GetTriggerValue();
			_animator.SetFloat(_triggerInputValueKey, input);

			if (input >= _triggerSprayPinch && !_isSpray)
			{
				IsSpray = true;
			}
			else if (input < _triggerSprayPinch && _isSpray)
			{
				IsSpray = false;
			}

			if (IsSpray)
			{
				float sprayStrength = (input - _triggerSprayPinch) / (_triggerInputValue.MaxTriggerValue - _triggerSprayPinch);
				_sprayForceUpdateEvent?.Invoke(sprayStrength);
			}
		}
	}
}