using GraffitiDrawingVR.Runtime.Core;
using GraffitiDrawingVR.Runtime.Interaction;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace GraffitiDrawingVR
{
	[Serializable]
	public class LookDetectionData
	{
		public float LookAngle;

		public LookMatchDirection MatchDirection;

		[Space]
		public Transform Target1;
		public Axis Target1Axis;

		[Space]
		public Transform Target2;
		public Axis Target2Asix;

		[Space]
		public UnityEvent StartLookingEvent;
		public UnityEvent StopLookingEvent;

		[HideInInspector]
		public bool LookingStarted;
	}
}
