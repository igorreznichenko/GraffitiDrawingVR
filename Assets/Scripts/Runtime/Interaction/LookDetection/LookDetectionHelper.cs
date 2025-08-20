using GraffitiDrawingVR.Runtime.Extensions;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Interaction
{
	public class LookDetectionHelper : MonoBehaviour
	{
		[SerializeField]
		private LookDetectionData[] _lookDetectionData;

		[SerializeField]
		private bool _isActive;

		public bool IsActive
		{
			get => _isActive;
			set
			{
				if (_isActive != value)
				{
					_isActive = value;
					OnActiveStateChangedEvent(value);
				}
			}
		}

		private void Update()
		{
			if (IsActive)
			{
				ProcessTracking();
			}
		}

		private void OnActiveStateChangedEvent(bool value)
		{
			if (!value)
			{
				foreach(var lookDetectionData in _lookDetectionData)
				{
					if (lookDetectionData.LookingStarted)
					{
						lookDetectionData.LookingStarted = false;
						lookDetectionData.StopLookingEvent?.Invoke();
					}
				}
			}
		}

		private void ProcessTracking()
		{
			foreach (var lookDetectionData in _lookDetectionData)
			{
				Vector3 lookDirection1 = lookDetectionData.Target1.GetDirectionByAxis(lookDetectionData.Target1Axis);
				Vector3 lookDirection2 = lookDetectionData.Target2.GetDirectionByAxis(lookDetectionData.Target2Asix);

				float angle = Vector3.Angle(lookDirection1, lookDirection2);

				if (lookDetectionData.MatchDirection == LookMatchDirection.OppositeDirection)
				{
					angle = 180 - angle;
				}

				if (angle <= lookDetectionData.LookAngle && !lookDetectionData.LookingStarted)
				{
					lookDetectionData.StartLookingEvent?.Invoke();
					lookDetectionData.LookingStarted = true;
				}
				else if (angle > lookDetectionData.LookAngle && lookDetectionData.LookingStarted)
				{
					lookDetectionData.StopLookingEvent?.Invoke();
					lookDetectionData.LookingStarted = false;
				}
			}
		}
	}
}
