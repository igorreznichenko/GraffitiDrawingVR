using GraffitiDrawingVR.Runtime.Constants;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace GraffitiDrawingVR.Runtime.Input
{
	public class GrabbableTriggerInputValue : MonoBehaviour, ITriggerInputValue
	{
		[SerializeField]
		private XRGrabInteractable _interactable;

		[SerializeField]
		private InputActionReference _leftTrigger;

		[SerializeField]
		private InputActionReference _rightTrigger;

		[SerializeField]
		private float _maxTriggerValue;

		public float MaxTriggerValue
		{
			get { return _maxTriggerValue; }
			set { _maxTriggerValue = value; }
		}

		public float GetTriggerValue()
		{
			float normalizedInput = GetNormalizedTriggerValue();

			return normalizedInput * MaxTriggerValue;
		}

		private float GetNormalizedTriggerValue()
		{
			if (_interactable.interactorsSelecting.Count > 0)
			{
				IXRSelectInteractor directInteractor = _interactable.firstInteractorSelecting;

				float triggerValue = GetTriggerValueByTag(directInteractor.transform.tag);

				return triggerValue;
			}

			return 0;
		}

		private float GetTriggerValueByTag(string tag)
		{
			switch (tag)
			{
				case ControllerTags.LEFT_CONTROLLER_TAG:
					{
						return _leftTrigger.action.ReadValue<float>();
					}
				case ControllerTags.RIGHT_CONTROLLER_TAG:
					{
						return _rightTrigger.action.ReadValue<float>();
					}
			}

			throw new Exception("Not valid tag!");
		}
	}
}