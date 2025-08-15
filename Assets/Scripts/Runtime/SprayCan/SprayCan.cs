using GraffitiDrawingVR.Runtime.Constants;
using GraffitiDrawingVR.Runtime.Drawing;
using GraffitiDrawingVR.Runtime.Input;
using GraffitiDrawingVR.Runtime.VFX;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace GraffitiDrawingVR.Runtime.SprayCanScripts
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

		[SerializeField]
		private Color _color;

		public Color Color
		{
			get { return _color; }
		}

		private ITriggerInputValue _triggerInputValue;

		private IDrawer _drawer;

		private ISprayCanVFX _vfx;

		private int _triggerInputValueKey = Animator.StringToHash(AnimatorVariableName.APRAY_CAN_TRIGGER_INPUT_VALUE);

		public bool IsSpray
		{
			get { return _sprayForce > 0; }
		}

		private float _sprayForce;

		public float SprayForce
		{
			get { return _sprayForce; }

			private set
			{
				if (_sprayForce != value)
				{
					float lastValue = _sprayForce;

					_sprayForce = value;

					if (lastValue == 0 && _sprayForce > 0)
					{
						OnStartSprayEventHandler();
					}
					else if (lastValue > 0 && _sprayForce == 0)
					{
						OnStopSprayEventHandler();
					}

					OnSprayForceChangedEventHandler(value);
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
			_drawer = GetComponentInChildren<IDrawer>();
			_vfx = GetComponentInChildren<ISprayCanVFX>();

			SetColor(_color);
		}

		private void OnStartSprayEventHandler()
		{
			_vfx.StartVFX();
			_startSprayEvent?.Invoke();
		}

		private void OnStopSprayEventHandler()
		{
			_vfx.StopVFX();
			_stopSprayEvent?.Invoke();
		}

		private void OnSprayForceChangedEventHandler(float force)
		{
			_vfx.UpdateSprayForce(force);
			_sprayForceUpdateEvent?.Invoke(force);
		}

		private void SetColor(Color color)
		{
			_drawer.SetColor(color);
			_vfx.SetColor(color);
		}

		private void Update()
		{
			ProcessTriggerInput();
		}

		private void ProcessTriggerInput()
		{
			float input = _triggerInputValue.GetTriggerValue();

			_animator.SetFloat(_triggerInputValueKey, input);

			SprayForce = Mathf.InverseLerp(_triggerSprayPinch, _triggerInputValue.MaxTriggerValue, input);

			if (IsSpray)
			{
				_drawer.Draw(SprayForce);
			}
		}
	}
}